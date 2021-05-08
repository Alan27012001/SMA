using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class UsuarioRol
    {
        public int IdUsuario { get; set; }
        public short IdRol { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
