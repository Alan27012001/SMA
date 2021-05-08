using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            Reporte = new HashSet<Reporte>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Reporte> Reporte { get; set; }
    }
}
