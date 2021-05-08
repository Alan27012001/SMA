using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class PantallaPermiso
    {
        public short IdPantalla { get; set; }
        public int IdPermiso { get; set; }

        public virtual Pantalla IdPantallaNavigation { get; set; }
        public virtual Permiso IdPermisoNavigation { get; set; }
    }
}
