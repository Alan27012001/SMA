using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Org.BouncyCastle.Math.EC.Rfc7748;
using SMA.Modelos;
using SMA.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;

namespace SMA.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogoController : BaseController
    {
        public CatalogoController(IConfiguration iconfig) : base(iconfig)
        {

        }

        #region Proyectos
        [HttpGet, Authorize]
        public IActionResult ObtenerProyectos(string nombre, string descripcion, bool todos, int numeroPaginacion, int cantidadPaginacion, string columnaOrdenamiento, bool reversaOrdenamiento)
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
                var proyectos = db.Proyecto.AsNoTracking().AsQueryable();

                if (!string.IsNullOrWhiteSpace(nombre))
                    proyectos = proyectos.Where(x => x.Nombre.Contains(nombre));
                if (!string.IsNullOrWhiteSpace(descripcion))
                    proyectos = proyectos.Where(x => x.Descripcion.Contains(descripcion));
                if (!todos)
                    proyectos = proyectos.Where(x => (bool)x.Activo);

                switch (columnaOrdenamiento)
                {
                    case "Nombre":
                        if (reversaOrdenamiento)
                            proyectos = proyectos.OrderByDescending(x => x.Nombre);
                        else
                            proyectos = proyectos.OrderBy(x => x.Nombre);
                        break;
                    case "Descripcion":
                        if (reversaOrdenamiento)
                            proyectos = proyectos.OrderByDescending(x => x.Descripcion);
                        else
                            proyectos = proyectos.OrderBy(x => x.Descripcion);
                        break;
                    case "Activo":
                        if (reversaOrdenamiento)
                            proyectos = proyectos.OrderByDescending(x => x.Activo);
                        else
                            proyectos = proyectos.OrderBy(x => x.Activo);
                        break;
                    default:
                        if (reversaOrdenamiento)
                            proyectos = proyectos.OrderByDescending(x => x.Nombre);
                        else
                            proyectos = proyectos.OrderBy(x => x.Nombre);
                        break;
                }

                var lista = proyectos.Select(x => new ProyectoViewModel
                {
                    Id = x.Id,
                    Nombre =  x.Nombre,
                    Descripcion = x.Descripcion,
                    Activo = x.Activo
                });

                return Ok(PaginacionConsulta.ObtenerPaginacion(lista, numeroPaginacion, cantidadPaginacion));
            }
        }

        [HttpGet, Authorize]
        public IActionResult ObtenerProyecto(int id)
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
                var proyecto = db.Proyecto.FirstOrDefault(x => x.Id == id);

                if(proyecto == null)
                    return BadRequest(constantes.proyecto.mensajes.proyectoNoEncontrado);

                var resultado = new ProyectoViewModel
                {
                    Id = proyecto.Id,
                    Nombre = proyecto.Nombre,
                    Descripcion = proyecto.Descripcion,
                    Activo = proyecto.Activo
                };

                return Ok(resultado);
            }
        }

        [HttpPost, Authorize]
        public IActionResult GuardarProyecto(ProyectoViewModel modelo)
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
                try
                {
                    db.Database.BeginTransaction();
                    if (modelo.Id > 0)
                    {
                        var proyecto = db.Proyecto.FirstOrDefault(x => x.Id == modelo.Id);
                        proyecto.Nombre = modelo.Nombre;
                        proyecto.Descripcion = modelo.Descripcion;
                        proyecto.Activo = modelo.Activo;
                    }
                    else
                    {
                        var proyecto = new Proyecto
                        {
                            Id = modelo.Id,
                            Nombre = modelo.Nombre,
                            Descripcion = modelo.Descripcion
                        };
                        proyecto.Activo = true;
                        db.Proyecto.Add(proyecto);
                        db.SaveChanges();
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
        #endregion

        #region MotivosReporte
        [HttpGet, Authorize]
        public IActionResult ObtenerMotivos(string motivo, string descripcion, bool todos, int numeroPaginacion, int cantidadPaginacion, string columnaOrdenamiento, bool reversaOrdenamiento)
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
                var motivos = db.Motivo.AsNoTracking().AsQueryable();

                if (!string.IsNullOrWhiteSpace(motivo))
                    motivos = motivos.Where(x => x.Motivo1.Contains(motivo));
                if (!string.IsNullOrWhiteSpace(descripcion))
                    motivos = motivos.Where(x => x.Descripcion.Contains(descripcion));
                if (!todos)
                    motivos = motivos.Where(x => (bool)x.Activo);

                switch (columnaOrdenamiento)
                {
                    case "Motivo":
                        if (reversaOrdenamiento)
                            motivos = motivos.OrderByDescending(x => x.Motivo1);
                        else
                            motivos = motivos.OrderBy(x => x.Motivo1);
                        break;
                    case "Descripcion":
                        if (reversaOrdenamiento)
                            motivos = motivos.OrderByDescending(x => x.Descripcion);
                        else
                            motivos = motivos.OrderBy(x => x.Descripcion);
                        break;
                    case "Activo":
                        if (reversaOrdenamiento)
                            motivos = motivos.OrderByDescending(x => x.Activo);
                        else
                            motivos = motivos.OrderBy(x => x.Activo);
                        break;
                    default:
                        if (reversaOrdenamiento)
                            motivos = motivos.OrderByDescending(x => x.Motivo1);
                        else
                            motivos = motivos.OrderBy(x => x.Motivo1);
                        break;
                }

                var lista = motivos.Select(x => new MotivoViewModel{
                    Id = x.Id,
                    Motivo = x.Motivo1,
                    Descripcion = x.Descripcion,
                    Activo = x.Activo
                });

                return Ok(PaginacionConsulta.ObtenerPaginacion(lista, numeroPaginacion, cantidadPaginacion));
            }
        }

        [HttpGet, Authorize]
        public IActionResult ObtenerMotivo(int id)
        {
            using (var db = new smafacpyaContext())
            {
                var valido = ValidarLogin();
                if(valido != HttpStatusCode.OK)
                {
                    if (valido == HttpStatusCode.Unauthorized)
                        return Unauthorized(constantes.mensajes.sesionExpirada);
                    else
                        return BadRequest(constantes.mensajes.error);
                }
                var motivo = db.Motivo.FirstOrDefault(x => x.Id == id);

                if (motivo == null)
                    return BadRequest(constantes.motivo.mensajes.motivoNoEncontrado);

                var resultado = new MotivoViewModel
                {
                    Id = motivo.Id,
                    Motivo = motivo.Motivo1,
                    Descripcion = motivo.Descripcion,
                    Activo = motivo.Activo
                };

                return Ok(resultado);
            }
        }

        [HttpPost, Authorize]
        public IActionResult GuardarMotivo(MotivoViewModel modelo)
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
                try
                {
                    db.Database.BeginTransaction();
                    if (modelo.Id > 0)
                    {
                        var motivo = db.Motivo.FirstOrDefault(x => x.Id == modelo.Id);
                        motivo.Motivo1 = modelo.Motivo;
                        motivo.Descripcion = modelo.Descripcion;
                        motivo.Activo = modelo.Activo;
                    }
                    else
                    {
                        var motivo = new Motivo
                        {
                            Id = modelo.Id,
                            Motivo1 = modelo.Motivo,
                            Descripcion = modelo.Descripcion,
                        };
                        motivo.Activo = true;
                        db.Motivo.Add(motivo);
                        db.SaveChanges();
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
        #endregion

    }
}
