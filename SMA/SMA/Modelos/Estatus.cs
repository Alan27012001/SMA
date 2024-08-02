using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class Estatus
    {
        public Estatus()
        {
            Reporte = new HashSet<Reporte>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Llave { get; set; }

        public virtual ICollection<Reporte> Reporte { get; set; }

        public string Prueba { get; set; }
    }
}
