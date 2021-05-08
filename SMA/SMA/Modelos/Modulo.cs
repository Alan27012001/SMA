using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class Modulo
    {
        public Modulo()
        {
            ModuloPantalla = new HashSet<ModuloPantalla>();
            RolModuloPantallaPermiso = new HashSet<RolModuloPantallaPermiso>();
        }

        public short Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
        public string Llave { get; set; }

        public virtual ICollection<ModuloPantalla> ModuloPantalla { get; set; }
        public virtual ICollection<RolModuloPantallaPermiso> RolModuloPantallaPermiso { get; set; }
    }
}
