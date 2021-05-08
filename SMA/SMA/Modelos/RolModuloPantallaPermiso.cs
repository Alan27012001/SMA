using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class RolModuloPantallaPermiso
    {
        public short IdRol { get; set; }
        public short IdModulo { get; set; }
        public short IdPantalla { get; set; }
        public int IdPermiso { get; set; }

        public virtual Modulo IdModuloNavigation { get; set; }
        public virtual Pantalla IdPantallaNavigation { get; set; }
        public virtual Permiso IdPermisoNavigation { get; set; }
        public virtual Rol IdRolNavigation { get; set; }
    }
}
