using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class Evidencia
    {
        public int Id { get; set; }
        public int? IdReporte { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public byte[] Imagen { get; set; }

        public virtual Reporte IdReporteNavigation { get; set; }
    }
}
