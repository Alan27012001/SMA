using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMA.ViewModels
{
    public abstract class PaginacionBase
    {
        public int PaginaActual { get; set; }
        public int PaginaTotal { get; set; }
        public int PaginaCantidad { get; set; }
        public int PaginaFilas { get; set; }
        public int PrimerFilaEnPagina
        {
            get { return (PaginaActual - 1) * PaginaCantidad + 1; }
        }
        public int UltimaFilaEnPagina
        {
            get { return Math.Min(PaginaActual * PaginaCantidad, PaginaFilas); }
        }
    }

    public class PaginacionResultado<T> : PaginacionBase where T : class
    {
        public IList<T> Resultados { get; set; }
        public PaginacionResultado()
        {
            Resultados = new List<T>();
        }
    }

    public static class PaginacionConsulta
    {
        public static PaginacionResultado<T> ObtenerPaginacion<T>(this IQueryable<T> consulta, int pagina, int cantidad) where T : class
        {
            var resultado = new PaginacionResultado<T>();
            if (pagina <= 0)
                pagina = 1;
            if (cantidad <= 0)
                cantidad = consulta.Count();
            if (cantidad <= 0)
                cantidad = 1;

            resultado.PaginaActual = pagina;
            resultado.PaginaCantidad = cantidad;
            resultado.PaginaFilas = consulta.Count();

            var paginaTotal = (double)resultado.PaginaFilas / resultado.PaginaCantidad;
            resultado.PaginaTotal = (int)Math.Ceiling(paginaTotal);

            if (resultado.PaginaActual > resultado.PaginaTotal && resultado.PaginaTotal > 0)
                return ObtenerPaginacion(consulta, resultado.PaginaTotal, resultado.PaginaCantidad);

            var omitir = (resultado.PaginaActual - 1) * resultado.PaginaCantidad;
            resultado.Resultados = consulta.Skip(omitir).Take(resultado.PaginaCantidad).ToList();

            return resultado;
        }
    }
}
