using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class Motivo
    {
        public Motivo()
        {
            Reporte = new HashSet<Reporte>();
        }

        public int Id { get; set; }
        public string Motivo1 { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Reporte> Reporte { get; set; }
    }
}
