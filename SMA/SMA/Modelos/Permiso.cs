using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class Permiso
    {
        public Permiso()
        {
            PantallaPermiso = new HashSet<PantallaPermiso>();
            RolModuloPantallaPermiso = new HashSet<RolModuloPantallaPermiso>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Llave { get; set; }

        public virtual ICollection<PantallaPermiso> PantallaPermiso { get; set; }
        public virtual ICollection<RolModuloPantallaPermiso> RolModuloPantallaPermiso { get; set; }
    }
}
