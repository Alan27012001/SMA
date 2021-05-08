using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SMA.Helpers;
using SMA.Modelos;
using SMA.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SMA.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeguridadController : BaseController
    {
        public SeguridadController(IConfiguration iconfig) : base(iconfig)
        {

        }

        #region login
        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel modelo)
        {
            using (var db = new smafacpyaContext())
            {
                db.Database.BeginTransaction();
                try
                {
                    var contraseñaHelper = new Contraseña();

                    if (string.IsNullOrWhiteSpace(modelo.Correo))
                        return BadRequest(constantes.seguridad.mensajes.correoVacio);

                    var usuarios = db.Usuario.AsNoTracking().Include(x => x.UsuarioRol).Where(x => x.Correo.Trim().Equals(modelo.Correo.Trim()) && x.Activo);

                    if (usuarios == null)
                        return BadRequest(constantes.seguridad.mensajes.usuarioNoEncontrado);
                    if (usuarios.Count() == 0)
                        return BadRequest(constantes.seguridad.mensajes.usuarioNoEncontrado);

                    var usuario = usuarios.FirstOrDefault();
                    var contraseña = contraseñaHelper.Encriptar(modelo.Contraseña);

                    if (!contraseñaHelper.CompararContraseña(contraseña, usuario.Contraseña))
                        return BadRequest(constantes.seguridad.mensajes.usuarioContrasenaIncorrecto);

                    var logins = db.UsuarioLogin.Where(x => x.Activo && x.IdUsuario == usuario.Id);
                    var hoy = DateTime.Now;
                    var vigencia = DateTime.Now.AddMonths(1);
                    var idLogin = Guid.NewGuid();
                    var roles = new List<Rol>();
                    var menu = ObtenerMenuPorUsuario(usuario.Id);

                    foreach (var login in logins)
                        login.Activo = false;

                    var loginNuevo = new UsuarioLogin
                    {
                        Id = idLogin,
                        IdUsuario = usuario.Id,
                        FechaCreacion = hoy,
                        FechaVigencia = vigencia,
                        Activo = true
                    };

                    if (usuario.UsuarioRol != null)
                        foreach (var ur in usuario.UsuarioRol)
                            roles.Add(db.Rol.FirstOrDefault(x => x.Id == ur.IdRol));

                    db.UsuarioLogin.Add(loginNuevo);
                    var token = GenerarToken(usuario, idLogin, roles, vigencia);
                    db.SaveChanges();
                    db.Database.CommitTransaction();

                    var usuarioViewModel = new UsuarioViewModel
                    {
                        Correo = usuario.Correo
                    };
                    var rolViewModel = roles.Select(x => new RolViewModel
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Descripcion = x.Descripcion,
                        Activo = x.Activo.HasValue ? (bool)x.Activo : false
                    });
                    return Ok(new
                    {
                        Usuario = usuarioViewModel,
                        Token = token,
                        Roles = rolViewModel,
                        Menu = menu
                    });
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    return BadRequest(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
        }

        [HttpPost, Authorize]
        public IActionResult Logout()
        {
            using (var db = new smafacpyaContext())
            {
                db.Database.BeginTransaction();
                try
                {
                    var idUsuario = ObtenerUsuarioId();
                    var logins = db.UsuarioLogin.Where(x => x.Activo && x.IdUsuario == idUsuario);
                    foreach (var login in logins)
                        login.Activo = false;

                    db.SaveChanges();
                    db.Database.CommitTransaction();

                    return Ok();
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    return BadRequest(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
        }
        #endregion

        #region Usuario
        [HttpPost, Authorize]
        public IActionResult GuardarUsuario(UsuarioViewModel modelo)
        {
            var hoy = DateTime.Now;
            using (var db = new smafacpyaContext())
            {
                var valido = ValidarLogin();
                if (valido != HttpStatusCode.OK)
                {
                    if (valido == HttpStatusCode.Unauthorized)
                        return Unauthorized(constantes.mensajes.sesionExpirada);
                    else
                        return BadRequest(constantes.mensajes.error);
                }
                try
                {
                    var idUsuario = ObtenerUsuarioId();
                    db.Database.BeginTransaction();
                    var contraseña = new Contraseña();

                    if (modelo.Id > 0)
                    {
                        var usuario = db.Usuario.Include(x => x.UsuarioRol).FirstOrDefault(x => x.Id == modelo.Id);
                        usuario.Nombre = modelo.Nombre;
                        usuario.ApellidoPaterno = modelo.ApellidoPaterno;
                        usuario.ApellidoMaterno = modelo.ApellidoMaterno;
                        usuario.FechaNacimiento = modelo.FechaNacimiento;
                        usuario.Correo = modelo.Correo;
                        usuario.FechaEdicion = hoy;
                        usuario.UsuarioEdicion = idUsuario;

                        if (!string.IsNullOrWhiteSpace(modelo.ContraseñaCadena))
                            usuario.Contraseña = contraseña.Encriptar(modelo.ContraseñaCadena);

                        if (usuario.Activo && !modelo.Activo)
                        {
                            usuario.FechaEliminacion = hoy;
                            usuario.UsuarioEliminacion = idUsuario;
                        }
                        usuario.Activo = modelo.Activo;

                        if (usuario.UsuarioRol != null)
                            db.UsuarioRol.RemoveRange(usuario.UsuarioRol);

                        foreach (var rolViewModel in modelo.Rol)
                        {
                            var rol = db.Rol.FirstOrDefault(x => x.Id == rolViewModel.Id);
                            db.UsuarioRol.Add(new UsuarioRol { IdRol = (short)rol.Id, IdUsuario = usuario.Id });
                        }
                    }
                    else
                    {
                        var usuario = new Usuario
                        {
                            Id = modelo.Id,
                            Nombre = modelo.Nombre,
                            ApellidoPaterno = modelo.ApellidoPaterno,
                            ApellidoMaterno = modelo.ApellidoMaterno,
                            Correo = modelo.Correo,
                            FechaNacimiento = modelo.FechaNacimiento
                        };
                        usuario.Activo = true;
                        usuario.Contraseña = contraseña.Encriptar(modelo.ContraseñaCadena);
                        usuario.FechaCreacion = hoy;
                        usuario.UsuarioCreacion = idUsuario;
                        db.Usuario.Add(usuario);
                        db.SaveChanges();

                        foreach (var rolViewModel in modelo.Rol)
                        {
                            var rol = db.Usuario.FirstOrDefault(x => x.Id == rolViewModel.Id);
                            db.UsuarioRol.Add(new UsuarioRol { IdRol = (short)rol.Id, IdUsuario = usuario.Id });
                        }
                    }

                    db.SaveChanges();
                    db.Database.CommitTransaction();

                    return Ok(constantes.mensajes.registroGuardado);
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    return BadRequest(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
        }

        [HttpGet, Authorize]
        public IActionResult ObtenerUsuarios(string nombre, string correo, bool todos, int numeroPaginacion, int cantidadPaginacion, string columnaOrdenamiento, bool reversaOrdenamiento)
        {
            using (var db = new smafacpyaContext())
            {
                var valido = ValidarLogin();
                if (valido != HttpStatusCode.OK)
                {
                    if (valido == HttpStatusCode.Unauthorized)
                        return Unauthorized(constantes.mensajes.sesionExpirada);
                    else
                        return BadRequest(constantes.mensajes.error);
                }
                var usuarios = db.Usuario.AsNoTracking().AsQueryable();

                if (!string.IsNullOrWhiteSpace(nombre))
                    usuarios = usuarios.Where(x => x.Nombre.Contains(nombre) || x.ApellidoPaterno.Contains(nombre) || x.ApellidoMaterno.Contains(nombre));
                if (!string.IsNullOrWhiteSpace(correo))
                    usuarios = usuarios.Where(x => x.Correo.Contains(correo));
                if (!todos)
                    usuarios = usuarios.Where(x => x.Activo);

                switch (columnaOrdenamiento)
                {
                    case "Nombre":
                        if (reversaOrdenamiento)
                            usuarios = usuarios.OrderByDescending(x => x.Nombre).ThenBy(x => x.ApellidoPaterno).ThenBy(x => x.ApellidoMaterno);
                        else
                            usuarios = usuarios.OrderBy(x => x.Nombre).ThenByDescending(x => x.ApellidoPaterno).ThenByDescending(x => x.ApellidoMaterno);
                        break;
                    case "Correo":
                        if (reversaOrdenamiento)
                            usuarios = usuarios.OrderByDescending(x => x.Correo);
                        else
                            usuarios = usuarios.OrderBy(x => x.Correo);
                        break;
                    case "Activo":
                        if (reversaOrdenamiento)
                            usuarios = usuarios.OrderByDescending(x => x.Activo);
                        else
                            usuarios = usuarios.OrderBy(x => x.Activo);
                        break;
                    case "FechaNacimiento":
                        if (reversaOrdenamiento)
                            usuarios = usuarios.OrderByDescending(x => x.FechaNacimiento);
                        else
                            usuarios = usuarios.OrderBy(x => x.FechaNacimiento);
                        break;
                    default:
                        if (reversaOrdenamiento)
                            usuarios = usuarios.OrderByDescending(x => x.Nombre).ThenBy(x => x.ApellidoPaterno).ThenBy(x => x.ApellidoMaterno);
                        else
                            usuarios = usuarios.OrderBy(x => x.Nombre).ThenByDescending(x => x.ApellidoPaterno).ThenByDescending(x => x.ApellidoMaterno);
                        break;
                }

                var lista = usuarios.Select(x => new UsuarioViewModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    ApellidoPaterno = x.ApellidoPaterno,
                    ApellidoMaterno = x.ApellidoMaterno,
                    Correo = x.Correo,
                    FechaNacimiento = x.FechaNacimiento,
                    Activo = x.Activo
                });

                return Ok(PaginacionConsulta.ObtenerPaginacion(lista, numeroPaginacion, cantidadPaginacion));
            }
        }

        [HttpGet, Authorize]
        public IActionResult ObtenerUsuario(int id)
        {
            using (var db = new smafacpyaContext())
            {
                var valido = ValidarLogin();
                if (valido != HttpStatusCode.OK)
                {
                    if (valido == HttpStatusCode.Unauthorized)
                        return Unauthorized(constantes.mensajes.sesionExpirada);
                    else
                        return BadRequest(constantes.mensajes.error);
                }
                var usuario = db.Usuario.Include(x => x.UsuarioRol).ThenInclude(x => x.IdRolNavigation).FirstOrDefault(x => x.Id == id);

                if (usuario == null)
                    return BadRequest(constantes.seguridad.mensajes.usuarioNoEncontrado);

                var resultado = new UsuarioViewModel
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    ApellidoPaterno = usuario.ApellidoPaterno,
                    ApellidoMaterno = usuario.ApellidoMaterno,
                    Correo = usuario.Correo,
                    FechaNacimiento = usuario.FechaNacimiento,
                    Activo = usuario.Activo,
                    Rol = usuario.UsuarioRol.Select(x => new RolViewModel
                    {
                        Id = x.IdRolNavigation.Id,
                        Nombre = x.IdRolNavigation.Nombre
                    }).ToList()
                };

                return Ok(resultado);
            }
        }
        #endregion

        #region Roles
        [HttpPost, Authorize]
        public IActionResult GuardarRol(RolViewModel modelo)
        {
            var hoy = DateTime.Now;
            using (var db = new smafacpyaContext())
            {
                var valido = ValidarLogin();
                if (valido != HttpStatusCode.OK)
                {
                    if (valido == HttpStatusCode.Unauthorized)
                        return Unauthorized(constantes.mensajes.sesionExpirada);
                    else
                        return BadRequest(constantes.mensajes.error);
                }
                try
                {
                    var idUsuario = ObtenerUsuarioId();
                    db.Database.BeginTransaction();
                    var contraseña = new Contraseña();

                    if (modelo.Id > 0)
                    {
                        var rol = db.Rol.Include(x => x.RolModuloPantallaPermiso).FirstOrDefault(x => x.Id == modelo.Id);
                        rol.Nombre = modelo.Nombre;
                        rol.Descripcion = modelo.Descripcion;
                        rol.FechaEdicion = hoy;
                        rol.UsuarioEdicion = idUsuario;

                        if ((bool)rol.Activo && !modelo.Activo)
                        {
                            rol.FechaEliminacion = hoy;
                            rol.UsuarioEliminacion = idUsuario;
                        }
                        rol.Activo = modelo.Activo;

                        if (rol.RolModuloPantallaPermiso != null)
                            db.RolModuloPantallaPermiso.RemoveRange(rol.RolModuloPantallaPermiso);

                        if (modelo.Menu != null)
                        {
                            foreach (var m in modelo.Menu)
                            {
                                var menu = db.Modulo.FirstOrDefault(x => x.Id == m.IdModulo);
                                if (m.Pantallas != null)
                                    foreach (var p in m.Pantallas)
                                        if (p.Permisos != null)
                                            foreach (var per in p.Permisos)
                                                db.RolModuloPantallaPermiso.Add(new RolModuloPantallaPermiso
                                                {
                                                    IdRol = rol.Id,
                                                    IdModulo = (short)m.IdModulo,
                                                    IdPantalla = (short)p.IdPantalla,
                                                    IdPermiso = per.IdPermiso
                                                });
                            }
                        }
                    }
                    else
                    {
                        var rol = new Rol
                        {
                            Id = (short)modelo.Id,
                            Nombre = modelo.Nombre,
                            Descripcion = modelo.Descripcion
                        };
                        rol.Activo = true;
                        rol.FechaCreacion = hoy;
                        rol.UsuarioCreacion = idUsuario;
                        db.Rol.Add(rol);
                        db.SaveChanges();

                        if (modelo.Menu != null)
                        {
                            foreach (var m in modelo.Menu)
                            {
                                var menu = db.Modulo.FirstOrDefault(x => x.Id == m.IdModulo);
                                if (m.Pantallas != null)
                                    foreach (var p in m.Pantallas)
                                        if (p.Permisos != null)
                                            foreach (var per in p.Permisos)
                                                db.RolModuloPantallaPermiso.Add(new RolModuloPantallaPermiso
                                                {
                                                    IdRol = rol.Id,
                                                    IdModulo = (short)m.IdModulo,
                                                    IdPantalla = (short)p.IdPantalla,
                                                    IdPermiso = per.IdPermiso
                                                });
                            }
                        }
                    }

                    db.SaveChanges();
                    db.Database.CommitTransaction();

                    return Ok(constantes.mensajes.registroGuardado);
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    return BadRequest(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
        }

        [HttpGet, Authorize]
        public IActionResult ObtenerRoles(string nombre, string descripcion, bool todos, int numeroPaginacion, int cantidadPaginacion, string columnaOrdenamiento, bool reversaOrdenamiento)
        {
            using (var db = new smafacpyaContext())
            {
                var valido = ValidarLogin();
                if (valido != HttpStatusCode.OK)
                {
                    if (valido == HttpStatusCode.Unauthorized)
                        return Unauthorized(constantes.mensajes.sesionExpirada);
                    else
                        return BadRequest(constantes.mensajes.error);
                }
                var roles = db.Rol.AsNoTracking().AsQueryable();

                if (!string.IsNullOrWhiteSpace(nombre))
                    roles = roles.Where(x => x.Nombre.Contains(nombre));
                if (!string.IsNullOrWhiteSpace(descripcion))
                    roles = roles.Where(x => x.Descripcion.Contains(descripcion));
                if (!todos)
                    roles = roles.Where(x => (bool)x.Activo);

                switch (columnaOrdenamiento)
                {
                    case "Nombre":
                        if (reversaOrdenamiento)
                            roles = roles.OrderByDescending(x => x.Nombre);
                        else
                            roles = roles.OrderBy(x => x.Nombre);
                        break;
                    case "Descripcion":
                        if (reversaOrdenamiento)
                            roles = roles.OrderByDescending(x => x.Descripcion);
                        else
                            roles = roles.OrderBy(x => x.Descripcion);
                        break;
                    case "Activo":
                        if (reversaOrdenamiento)
                            roles = roles.OrderByDescending(x => x.Activo);
                        else
                            roles = roles.OrderBy(x => x.Activo);
                        break;
                    default:
                        if (reversaOrdenamiento)
                            roles = roles.OrderByDescending(x => x.Nombre);
                        else
                            roles = roles.OrderBy(x => x.Nombre);
                        break;
                }

                var lista = roles.Select(x => new RolViewModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Activo = !x.Activo.HasValue ? false : (bool)x.Activo
                });

                return Ok(PaginacionConsulta.ObtenerPaginacion(lista, numeroPaginacion, cantidadPaginacion));
            }
        }

        [HttpGet, Authorize]
        public IActionResult ObtenerRol(int id)
        {
            using (var db = new smafacpyaContext())
            {
                var valido = ValidarLogin();
                if (valido != HttpStatusCode.OK)
                {
                    if (valido == HttpStatusCode.Unauthorized)
                        return Unauthorized(constantes.mensajes.sesionExpirada);
                    else
                        return BadRequest(constantes.mensajes.error);
                }
                var rol = db.Rol.Include(x => x.RolModuloPantallaPermiso).FirstOrDefault(x => x.Id == id);

                if (rol == null)
                    return BadRequest(constantes.seguridad.mensajes.usuarioNoEncontrado);

                var resultado = new RolViewModel
                {
                    Id = rol.Id,
                    Nombre = rol.Nombre,
                    Descripcion = rol.Descripcion,
                    Activo = (bool)rol.Activo,
                    Menu = ObtenerMenuPorRol(rol.Id)
                };

                return Ok(resultado);
            }
        }

        [HttpGet, Authorize]
        public IActionResult ObtenerMenus()
        {
            using (var db = new smafacpyaContext())
            {
                var menu = new List<MenuViewModel>();
                var modulos = db.Modulo.Where(x => (bool)x.Activo).ToList();

                foreach (var modulo in modulos)
                {
                    var idPantalla = db.ModuloPantalla.Where(y => y.IdModulo == modulo.Id).Select(y => y.IdPantalla).ToArray();
                    var opcion = new MenuViewModel
                    {
                        IdModulo = modulo.Id,
                        Modulo = modulo.Nombre,
                        Llave = modulo.Llave,
                        Pantallas = new List<PantallaViewModel>()
                    };
                    opcion.Pantallas.AddRange(
                        db.Pantalla.
                        Where(x => (bool)x.Activo && idPantalla.Contains(x.Id)).
                        Select(x => new PantallaViewModel
                        {
                            IdPantalla = x.Id,
                            Pantalla = x.Nombre,
                            Llave = x.Llave
                        })
                    );

                    foreach (var pantalla in opcion.Pantallas)
                    {
                        var idPermiso = db.PantallaPermiso.Where(y => y.IdPantalla == pantalla.IdPantalla).Select(y => y.IdPermiso).ToArray();
                        pantalla.Permisos = new List<PermisoViewModel>();
                        pantalla.Permisos = db.Permiso.Where(x => idPermiso.Contains(x.Id)).Select(x => new PermisoViewModel
                        {
                            IdPermiso = x.Id,
                            Permiso = x.Nombre,
                            Descripcion = x.Descripcion,
                            Llave = x.Llave
                        }).ToList();
                    }

                    menu.Add(opcion);
                }

                return Ok(menu);
            }
        }
        #endregion

        #region Recuperar Contraseña
        [HttpPost]
        public IActionResult RecuperarContrasena(LoginViewModel modelo)
        {
            var hoy = DateTime.Now;
            using (var db = new smafacpyaContext())
            {
                try
                {
                    if (modelo == null)
                        return BadRequest(constantes.seguridad.mensajes.correoVacio);
                    if (modelo.Correo == null)
                        return BadRequest(constantes.seguridad.mensajes.correoVacio);

                    var usuarios = db.Usuario.Where(x => !string.IsNullOrWhiteSpace(x.Correo) && x.Activo);
                    var usuario = usuarios.FirstOrDefault(x => x.Correo.Trim().Equals(modelo.Correo.Trim()));

                    if (usuario == null)
                        return BadRequest(constantes.seguridad.mensajes.usuarioNoEncontrado);

                    var registros = db.UsuarioContraseña.Where(x => x.Activo && x.IdUsuario == usuario.Id);
                    if (registros != null)
                        foreach (var r in registros)
                            r.Activo = false;
                    db.SaveChanges();

                    var id = Guid.NewGuid();

                    db.UsuarioContraseña.Add(new UsuarioContraseña
                    {
                        Id = id,
                        IdUsuario = usuario.Id,
                        FechaCreacion = hoy,
                        FechaVigencia = hoy.AddMinutes(constantes.recuperarContrasena.minutosActivo),
                        Activo = true
                    });
                    db.SaveChanges();

                    var link = constantes.linkWeb + "/restablecerContrasena/" + Encriptar(id.ToString());
                    var mensaje = constantes.recuperarContrasena.correo.mensaje.Replace("@Link", link);
                    mensaje = mensaje.Replace("@Ruta", constantes.linkWeb);
                    var envio = EnviarCorreo(usuario.Nombre + " " + usuario.ApellidoPaterno, usuario.Correo, constantes.recuperarContrasena.correo.asunto, mensaje);
                    if (envio == null)
                        return Ok(constantes.seguridad.mensajes.correoContrasena);
                    return BadRequest(string.IsNullOrWhiteSpace(envio) ? constantes.mensajes.error : envio);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
        }

        [HttpGet]
        public IActionResult RestablecerContrasena(string id)
        {
            var hoy = DateTime.Now;
            using (var db = new smafacpyaContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(id))
                        return BadRequest(constantes.seguridad.mensajes.urlNoEncontrado);

                    var registros = db.UsuarioContraseña.Where(x => x.Activo && x.FechaVigencia <= hoy);
                    if (registros != null)
                        foreach (var r in registros)
                            r.Activo = false;
                    db.SaveChanges();

                    var registro = db.UsuarioContraseña.AsNoTracking().FirstOrDefault(x => x.Id == new Guid(DesEncriptar(id)) && x.Activo);

                    if (registro == null)
                        return BadRequest(constantes.seguridad.mensajes.urlNoEncontrado);

                    return Ok(new UsuarioViewModel
                    {
                        Id = registro.IdUsuario
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
        }

        [HttpPost]
        public IActionResult ActualizarContrasena(LoginViewModel modelo)
        {
            var hoy = DateTime.Now;
            using (var db = new smafacpyaContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(modelo.Recuperar))
                        return BadRequest(constantes.seguridad.mensajes.urlNoEncontrado);
                    if (string.IsNullOrWhiteSpace(modelo.Contraseña))
                        return BadRequest(constantes.seguridad.mensajes.contrasenaNoValida);

                    var registros = db.UsuarioContraseña.Where(x => x.Activo && x.FechaVigencia <= hoy);
                    if (registros != null)
                        foreach (var r in registros)
                            r.Activo = false;
                    db.SaveChanges();

                    var registro = db.UsuarioContraseña.FirstOrDefault(x => x.Id == new Guid(DesEncriptar(modelo.Recuperar)) && x.Activo);

                    if (registro == null)
                        return BadRequest(constantes.seguridad.mensajes.urlNoEncontrado);

                    registro.Activo = false;
                    var usuario = db.Usuario.FirstOrDefault(x => x.Id == registro.IdUsuario);
                    var contraseña = new Contraseña();

                    if (usuario == null)
                        return BadRequest(constantes.seguridad.mensajes.usuarioNoEncontrado);

                    usuario.Contraseña = contraseña.Encriptar(modelo.Contraseña);

                    db.SaveChanges();
                    return Ok(constantes.mensajes.registroGuardado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
        }
        #endregion

        #region Menu y Permisos
        [HttpGet, Authorize]
        public IActionResult ObtenerPermisos(string llavePantalla)
        {
            using (var db = new smafacpyaContext())
            {
                var idUsuario = ObtenerUsuarioId();
                var permisos = new List<Permiso>();
                var usuario = db.Usuario.Include(x => x.UsuarioRol).FirstOrDefault(x => x.Id == idUsuario);
                var pantalla = db.Pantalla.FirstOrDefault(x => x.Llave.Equals(llavePantalla));
                var valido = ValidarLogin();

                if (valido != HttpStatusCode.OK)
                {
                    if (valido == HttpStatusCode.Unauthorized)
                        return Unauthorized(constantes.mensajes.sesionExpirada);
                    else
                        return BadRequest(constantes.mensajes.error);
                }

                if (pantalla == null)
                    return BadRequest(constantes.seguridad.mensajes.pantallaNoEncontrado);
                if (usuario == null)
                    return BadRequest(constantes.seguridad.mensajes.usuarioNoEncontrado);
                if (usuario.UsuarioRol == null)
                    return BadRequest(constantes.seguridad.mensajes.rolNoEncontrado);

                var idPantalla = pantalla.Id;
                var idRol = usuario.UsuarioRol.Select(x => x.IdRol).ToArray();
                if (idRol == null)
                    return BadRequest(constantes.seguridad.mensajes.rolNoEncontrado);
                if (idRol.Length == 0)
                    return BadRequest(constantes.seguridad.mensajes.rolNoEncontrado);

                permisos = db.RolModuloPantallaPermiso.
                    Include(x => x.IdPermisoNavigation).
                    Where(x => idRol.Contains(x.IdRol) && x.IdPantalla == idPantalla).
                    Select(x => x.IdPermisoNavigation).ToList();
                if (permisos == null)
                    permisos = new List<Permiso>();

                var menu = ObtenerMenuPorUsuario(ObtenerUsuarioId());

                return Ok(new
                {
                    permisos,
                    menu
                });
            }
        }
        #endregion

        #region Private
        private List<MenuViewModel> ObtenerMenuPorUsuario(int idUsuario)
        {
            using (var db = new smafacpyaContext())
            {
                var menu = new List<MenuViewModel>();
                var usuario = db.Usuario.Include(x => x.UsuarioRol).FirstOrDefault(x => x.Id == idUsuario);

                if (usuario == null)
                    return menu;
                if (usuario.UsuarioRol == null)
                    return menu;

                var idRol = usuario.UsuarioRol.Select(x => x.IdRol).ToArray();
                if (idRol == null)
                    return menu;
                if (idRol.Length == 0)
                    return menu;

                var permisos = db.RolModuloPantallaPermiso.Where(x => idRol.Contains(x.IdRol));
                var idModulo = permisos.Select(z => z.IdModulo).ToArray();
                var modulos = db.Modulo.Where(x => (bool)x.Activo && idModulo.Contains(x.Id)).ToList();

                foreach (var modulo in modulos)
                {
                    var idPantalla = permisos.Where(y => y.IdModulo == modulo.Id).Select(z => z.IdPantalla).ToArray();
                    var opcion = new MenuViewModel
                    {
                        IdModulo = modulo.Id,
                        Modulo = modulo.Nombre,
                        Llave = modulo.Llave,
                        Pantallas = new List<PantallaViewModel>()
                    };
                    var pantallas = db.Pantalla.
                        Where(x => (bool)x.Activo && idPantalla.Contains(x.Id)).
                        Select(x => new PantallaViewModel
                        {
                            IdPantalla = x.Id,
                            Pantalla = x.Nombre,
                            Llave = x.Llave,
                            Permisos = new List<PermisoViewModel>()
                        }).ToList();

                    foreach (var pantalla in pantallas)
                    {
                        var idPermiso = permisos.Where(y => y.IdModulo == modulo.Id && y.IdPantalla == pantalla.IdPantalla).Select(z => z.IdPermiso).ToArray();
                        pantalla.Permisos = db.Permiso.Where(x => idPermiso.Contains(x.Id)).Select(x => new PermisoViewModel
                        {
                            IdPermiso = x.Id,
                            Permiso = x.Nombre,
                            Descripcion = x.Descripcion,
                            Llave = x.Llave
                        }).ToList();

                        opcion.Pantallas.Add(pantalla);
                    }

                    menu.Add(opcion);
                }

                return menu;
            }
        }

        private List<MenuViewModel> ObtenerMenuPorRol(int idRol)
        {
            using (var db = new smafacpyaContext())
            {
                var menu = new List<MenuViewModel>();

                if (idRol == 0)
                    return menu;

                var permisos = db.RolModuloPantallaPermiso.Where(x => x.IdRol == idRol);
                var idModulo = permisos.Select(z => z.IdModulo).ToArray();
                var modulos = db.Modulo.Where(x => (bool)x.Activo && idModulo.Contains(x.Id)).ToList();

                foreach (var modulo in modulos)
                {
                    var idPantalla = permisos.Where(y => y.IdModulo == modulo.Id).Select(z => z.IdPantalla).ToArray();
                    var opcion = new MenuViewModel
                    {
                        IdModulo = modulo.Id,
                        Modulo = modulo.Nombre,
                        Llave = modulo.Llave,
                        Pantallas = new List<PantallaViewModel>()
                    };
                    var pantallas = db.Pantalla.
                        Where(x => (bool)x.Activo && idPantalla.Contains(x.Id)).
                        Select(x => new PantallaViewModel
                        {
                            IdPantalla = x.Id,
                            Pantalla = x.Nombre,
                            Llave = x.Llave,
                            Permisos = new List<PermisoViewModel>()
                        }).ToList();

                    foreach (var pantalla in pantallas)
                    {
                        var idPermiso = permisos.Where(y => y.IdModulo == modulo.Id && y.IdPantalla == pantalla.IdPantalla).Select(z => z.IdPermiso).ToArray();
                        pantalla.Permisos = db.Permiso.Where(x => idPermiso.Contains(x.Id)).Select(x => new PermisoViewModel
                        {
                            IdPermiso = x.Id,
                            Permiso = x.Nombre,
                            Descripcion = x.Descripcion,
                            Llave = x.Llave
                        }).ToList();

                        opcion.Pantallas.Add(pantalla);
                    }

                    menu.Add(opcion);
                }

                return menu;
            }
        }

        private string GenerarToken(Usuario usuario, Guid idLogin, List<Rol> roles, DateTime vigencia)
        {
            var header = new System.IdentityModel.Tokens.Jwt.JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("7Yi90?r$ft%.op1=yui")
                    ),
                    SecurityAlgorithms.HmacSha256)
            );

            var claims = new List<Claim>();
            if (roles != null)
                foreach (var rol in roles)
                    claims.Add(new Claim(ClaimTypes.Role, rol.Nombre));

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, idLogin.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, usuario.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, string.Concat(usuario.Nombre, " ", usuario.ApellidoPaterno, " ", usuario.ApellidoMaterno)));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.Correo));

            var payload = new JwtPayload(
                issuer: "yunntech",
                audience: "yunntech",
                claims: claims,
                notBefore: DateTime.Now,
                expires: vigencia
            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}