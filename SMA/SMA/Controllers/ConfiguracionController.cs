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
    public class ConfiguracionController : BaseController
    {
        public ConfiguracionController(IConfiguration iconfig) : base(iconfig)
        {

        }

        #region EstatusReporte
        [HttpGet, Authorize]
        public IActionResult ObtenerEstatusReporte(string nombre, int numeroPaginacion, int cantidadPaginacion, string columnaOrdenamiento, bool reversaOrdenamiento)
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
                var estatus = db.Estatus.AsNoTracking().AsQueryable();

                if (!string.IsNullOrWhiteSpace(nombre))
                    estatus = estatus.Where(x => x.Nombre.Contains(nombre));

                switch (columnaOrdenamiento)
                {
                    case "Nombre":
                        if (reversaOrdenamiento)
                            estatus = estatus.OrderByDescending(x => x.Nombre);
                        else
                            estatus = estatus.OrderBy(x => x.Nombre);
                        break;
                    default:
                        if (reversaOrdenamiento)
                            estatus = estatus.OrderByDescending(x => x.Nombre);
                        else
                            estatus = estatus.OrderBy(x => x.Nombre);
                        break;
                }

                var lista = estatus.Select(x => new EstatusViewModel
                { 
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Llave = x.Llave
                });

                return Ok(PaginacionConsulta.ObtenerPaginacion(lista, numeroPaginacion, cantidadPaginacion));
            } 
        }
        #endregion
    }
}
