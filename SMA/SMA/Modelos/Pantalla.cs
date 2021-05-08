using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class Pantalla
    {
        public Pantalla()
        {
            PantallaPermiso = new HashSet<PantallaPermiso>();
            RolModuloPantallaPermiso = new HashSet<RolModuloPantallaPermiso>();
        }

        public short Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ruta { get; set; }
        public bool? Activo { get; set; }
        public string Llave { get; set; }

        public virtual ICollection<PantallaPermiso> PantallaPermiso { get; set; }
        public virtual ICollection<RolModuloPantallaPermiso> RolModuloPantallaPermiso { get; set; }
    }
}
