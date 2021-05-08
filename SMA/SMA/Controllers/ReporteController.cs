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
    public class ReporteController : BaseController
    {
        public ReporteController(IConfiguration iconfig) : base(iconfig)
        {

        }

        #region ReporteUsuario
        [HttpGet, Authorize]
        public IActionResult ObtenerReportesUsuario(string folio, int idEstatusReporte, int numeroPaginacion, int cantidadPaginacion, string columnaOrdenamiento, bool reversaOrdenamiento)
        {
            using (var db = new smafacpyaContext())
            {
                var idUsuario = ObtenerUsuarioId();
                var valido = ValidarLogin();
                if (valido != HttpStatusCode.OK)
                {
                    if (valido == HttpStatusCode.Unauthorized)
                        return Unauthorized(constantes.mensajes.sesionExpirada);
                    else
                        return BadRequest(constantes.mensajes.error);
                }
                var reportes = db.Reporte.Include(x => x.IdMotivoNavigation).AsNoTracking().Where(x => x.IdUsuarioAsignacion == idUsuario);

                if (!string.IsNullOrWhiteSpace(folio))
                    reportes = reportes.Where(x => x.Folio.Contains(folio));
                if (idEstatusReporte > 0)
                    reportes = reportes.Where(x => x.IdEstatusReporte == idEstatusReporte);

                switch (columnaOrdenamiento)
                {
                    case "Folio":
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.Folio);
                        else
                            reportes = reportes.OrderBy(x => x.Folio);
                        break;
                    case "Motivo":
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.IdMotivoNavigation.Motivo1);
                        else
                            reportes = reportes.OrderBy(x => x.IdMotivoNavigation.Motivo1);
                        break;
                    case "FechaReporte":
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.FechaReporte);
                        else
                            reportes = reportes.OrderBy(x => x.FechaReporte);
                        break;
                    case "FechaFinalizacion":
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.FechaFinalizacion);
                        else
                            reportes = reportes.OrderBy(x => x.FechaFinalizacion);
                        break;
                    case "EstatusReporte":
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.IdEstatusReporteNavigation.Nombre);
                        else
                            reportes = reportes.OrderBy(x => x.IdEstatusReporteNavigation.Nombre);
                        break;
                    default:
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.Folio);
                        else
                            reportes = reportes.OrderBy(x => x.Folio);
                        break;
                }

                var lista = reportes.Select(x => new ReporteViewModel
                {
                    Id = x.Id,
                    Folio = x.Folio,
                    FechaReporte = x.FechaReporte,
                    FechaFinalizacion = x.FechaFinalizacion,
                    Motivo = x.IdMotivoNavigation == null ? new MotivoViewModel() : new MotivoViewModel
                    {
                        Id = x.IdMotivoNavigation.Id,
                        Motivo = x.IdMotivoNavigation.Motivo1
                    },
                    Proyecto = x.IdProyectoNavigation == null ? new ProyectoViewModel() : new ProyectoViewModel
                    {
                        Id = x.IdProyectoNavigation.Id,
                        Nombre = x.IdProyectoNavigation.Nombre
                    },
                    Estatus = x.IdEstatusReporteNavigation == null ? new EstatusViewModel() : new EstatusViewModel
                    {
                        Id = x.IdEstatusReporteNavigation.Id,
                        Nombre = x.IdEstatusReporteNavigation.Nombre
                    }
                });

                return Ok(PaginacionConsulta.ObtenerPaginacion(lista, numeroPaginacion, cantidadPaginacion));
            }
        }

        [HttpGet, Authorize]
        public IActionResult ObtenerReporteUsuarioPorId(int id)
        {
            using (var db = new smafacpyaContext())
            {
                var reporte = db.Reporte.Include(x => x.IdMotivoNavigation).Include(x => x.IdProyectoNavigation).AsNoTracking().FirstOrDefault(x => x.Id == id);

                if (reporte == null)
                    return BadRequest(constantes.reporte.mensajes.reporteNoEncontrado);

                var resultado = new ReporteViewModel
                {
                    Id = reporte.Id,
                    Folio = reporte.Folio,
                    IdMotivo = reporte.IdMotivo,
                    IdProyecto = reporte.IdProyecto,
                    FechaReporte = reporte.FechaReporte,
                    ComentarioReporte = reporte.ComentarioReporte,
                    IdEstatusReporte = reporte.IdEstatusReporte,
                    FechaAsignacion = reporte.FechaAsignacion,
                    ComentarioAsignacion = reporte.ComentarioAsignacion,
                    FechaFinalizacion = reporte.FechaFinalizacion,
                    ComentarioFinalizacion = reporte.ComentarioFinalizacion,
                    Motivo = reporte.IdMotivoNavigation == null ? null : new MotivoViewModel
                    {
                        Id = reporte.IdMotivoNavigation.Id,
                        Motivo = reporte.IdMotivoNavigation.Motivo1
                    },
                    Proyecto = reporte.IdProyectoNavigation == null ? null : new ProyectoViewModel
                    {
                        Id = reporte.IdProyectoNavigation.Id,
                        Nombre = reporte.IdProyectoNavigation.Nombre
                    },
                    Estatus = reporte.IdEstatusReporteNavigation == null ? null : new EstatusViewModel
                    {
                        Id = reporte.IdEstatusReporteNavigation.Id,
                        Nombre = reporte.IdEstatusReporteNavigation.Nombre
                    }
                };

                return Ok(resultado);
            }
        }

        #endregion

        #region ReporteAdministrador
        [HttpGet, Authorize]
        public IActionResult ObtenerReportesAdministrador(string folio, int idMotivo, int idProyecto, int idEstatusReporte, int numeroPaginacion, int cantidadPaginacion, string columnaOrdenamiento, bool reversaOrdenamiento)
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
                var reportes = db.Reporte.Include(x => x.IdMotivoNavigation).Include(x => x.IdProyectoNavigation).AsNoTracking().AsQueryable();

                if (!string.IsNullOrWhiteSpace(folio))
                    reportes = reportes.Where(x => x.Folio.Contains(folio));
                if (idMotivo > 0)
                    reportes = reportes.Where(x => x.IdMotivo == idMotivo);
                if (idProyecto > 0)
                    reportes = reportes.Where(x => x.IdProyecto == idProyecto);
                if (idEstatusReporte > 0)
                    reportes = reportes.Where(x => x.IdEstatusReporte == idEstatusReporte);

                switch (columnaOrdenamiento)
                {
                    case "Folio":
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.Folio);
                        else
                            reportes = reportes.OrderBy(x => x.Folio);
                        break;
                    case "Motivo":
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.IdMotivoNavigation.Motivo1);
                        else
                            reportes = reportes.OrderBy(x => x.IdMotivoNavigation.Motivo1);
                        break;
                    case "Proyecto":
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.IdProyectoNavigation.Nombre);
                        else
                            reportes = reportes.OrderBy(x => x.IdProyectoNavigation.Nombre);
                        break;
                    case "FechaReporte":
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.FechaReporte);
                        else
                            reportes = reportes.OrderBy(x => x.FechaReporte);
                        break;
                    case "Estatus del Reporte":
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.IdEstatusReporteNavigation.Nombre);
                        else
                            reportes = reportes.OrderBy(x => x.IdEstatusReporteNavigation.Nombre);
                        break;
                    default:
                        if (reversaOrdenamiento)
                            reportes = reportes.OrderByDescending(x => x.Folio);
                        else
                            reportes = reportes.OrderBy(x => x.Folio);
                        break;
                }

                var lista = reportes.Select(x => new ReporteViewModel
                {
                    Id = x.Id,
                    Folio = x.Folio,
                    FechaReporte = x.FechaReporte,
                    Motivo = x.IdMotivoNavigation == null ? new MotivoViewModel() : new MotivoViewModel
                    {
                        Id = x.IdMotivoNavigation.Id,
                        Motivo = x.IdMotivoNavigation.Motivo1
                    },
                    Proyecto = x.IdProyectoNavigation == null ? new ProyectoViewModel() : new ProyectoViewModel
                    {
                        Id = x.IdProyectoNavigation.Id,
                        Nombre = x.IdProyectoNavigation.Nombre
                    },
                    Estatus = x.IdEstatusReporteNavigation == null ? new EstatusViewModel() : new EstatusViewModel
                    {
                        Id = x.IdEstatusReporteNavigation.Id,
                        Nombre = x.IdEstatusReporteNavigation.Nombre
                    }
                });

                return Ok(PaginacionConsulta.ObtenerPaginacion(lista, numeroPaginacion, cantidadPaginacion));
            }
        }

        [HttpGet, Authorize]
        public IActionResult ObtenerReporteAdministradorPorId(int id)
        {
            using (var db = new smafacpyaContext())
            {
                var reporte = db.Reporte.Include(x => x.IdMotivoNavigation)
                                        .Include(x => x.IdProyectoNavigation)
                                        .Include(x => x.IdEstatusReporteNavigation).AsNoTracking().FirstOrDefault(x => x.Id == id);

                if (reporte == null)
                    return BadRequest(constantes.reporte.mensajes.reporteNoEncontrado);

                var resultado = new ReporteViewModel
                {
                    Id = reporte.Id,
                    Folio = reporte.Folio,
                    IdMotivo = reporte.IdMotivo,
                    IdProyecto = reporte.IdProyecto,
                    FechaReporte = reporte.FechaReporte,
                    ComentarioReporte = reporte.ComentarioReporte,
                    IdEstatusReporte = reporte.IdEstatusReporte,
                    IdUsuarioAsignacion = reporte.IdUsuarioAsignacion,
                    FechaAsignacion = reporte.FechaAsignacion,
                    ComentarioAsignacion = reporte.ComentarioAsignacion,
                    FechaFinalizacion = reporte.FechaFinalizacion,
                    ComentarioFinalizacion = reporte.ComentarioFinalizacion,
                    Motivo = reporte.IdMotivoNavigation == null ? null : new MotivoViewModel
                    {
                        Id = reporte.IdMotivoNavigation.Id,
                        Motivo = reporte.IdMotivoNavigation.Motivo1
                    },
                    Proyecto = reporte.IdProyecto == null ? null : new ProyectoViewModel
                    {
                        Id = reporte.IdProyectoNavigation.Id,
                        Nombre = reporte.IdProyectoNavigation.Nombre
                    },
                    Estatus = reporte.IdEstatusReporteNavigation == null ? null : new EstatusViewModel
                    {
                        Id = reporte.IdEstatusReporteNavigation.Id,
                        Nombre = reporte.IdEstatusReporteNavigation.Nombre
                    },
                    Usuario = reporte.IdUsuarioAsignacionNavigation == null ? null : new UsuarioViewModel
                    {
                        Id = reporte.IdUsuarioAsignacionNavigation.Id,
                        Nombre = reporte.IdUsuarioAsignacionNavigation.Nombre
                    }
                };
                
                return Ok(resultado);
            }
        }

        [HttpPost, Authorize]
        public IActionResult AsignarReporte(ReporteViewModel modelo)
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

                    if (modelo.Id > 0)
                    {
                        var reporte = db.Reporte.FirstOrDefault(x => x.Id == modelo.Id);

                        reporte.IdEstatusReporte = 2;
                        reporte.UsuarioEdicion = idUsuario;
                        reporte.FechaEdicion = hoy;
                        //checar esto lo puse setiado por el momento
                        reporte.IdUsuarioAsignacion = 2;
                        reporte.FechaAsignacion = modelo.FechaAsignacion;
                        reporte.ComentarioAsignacion = modelo.ComentarioAsignacion;
                    }
                    db.SaveChanges();
                    db.Database.CommitTransaction();
                    return Ok(constantes.mensajes.registroGuardado);
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    return BadRequest(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                    throw;
                }
            }
        }

        [HttpPost, Authorize]
        public IActionResult CerrarReporte(ReporteViewModel modelo)
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

                    if (modelo.Id > 0)
                    {
                        var reporte = db.Reporte.FirstOrDefault(x => x.Id == modelo.Id);

                        reporte.IdEstatusReporte = 3;
                        reporte.UsuarioEdicion = idUsuario;
                        reporte.FechaEdicion = hoy;
                        reporte.FechaFinalizacion = modelo.FechaFinalizacion;
                        reporte.ComentarioFinalizacion = modelo.ComentarioFinalizacion;
                    }
                    db.SaveChanges();
                    db.Database.CommitTransaction();
                    return Ok(constantes.mensajes.registroGuardado);
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    return BadRequest(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                    throw;
                }
            }
        }
        #endregion

        #region General
        [HttpPost, Authorize]
        public IActionResult GuardarReporte(ReporteViewModel modelo)
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
                    if (modelo.Id > 0)
                    {
                        var reporte = db.Reporte.FirstOrDefault(x => x.Id == modelo.Id);

                        reporte.IdMotivo = modelo.IdMotivo;
                        reporte.IdProyecto = modelo.IdProyecto;
                        reporte.FechaReporte = modelo.FechaReporte;
                        reporte.ComentarioReporte = modelo.ComentarioReporte;
                        reporte.UsuarioEdicion = idUsuario;
                        reporte.FechaEdicion = hoy;
                        reporte.IdUsuarioAsignacion = idUsuario;
                        reporte.FechaAsignacion = modelo.FechaAsignacion;
                        reporte.ComentarioAsignacion = modelo.ComentarioAsignacion;
                        reporte.FechaFinalizacion = modelo.FechaFinalizacion;
                        reporte.ComentarioFinalizacion = modelo.ComentarioFinalizacion;
                    }
                    else
                    {
                        var reporte = new Reporte
                        {
                            IdMotivo = modelo.IdMotivo,
                            IdProyecto = modelo.IdProyecto,
                            FechaReporte = modelo.FechaReporte,
                            ComentarioReporte = modelo.ComentarioReporte,
                            IdEstatusReporte = 1,
                            UsuarioCreacion = idUsuario,
                            FechaCreacion = hoy
                        };
                        db.Reporte.Add(reporte);
                        db.SaveChanges();
                        string formato = "RP00" ;
                        reporte.Folio = string.Concat(formato + reporte.Id.ToString());
                    }
                    db.SaveChanges();
                    db.Database.CommitTransaction();
                    return Ok(constantes.mensajes.registroGuardado);
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    return BadRequest(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                    throw;
                }
            }
        }
        #endregion
    }
}
