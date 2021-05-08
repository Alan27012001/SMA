using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class RolApp
    {
        public RolApp()
        {
            Usuario = new HashSet<Usuario>();
        }

        public short Id { get; set; }
        public string Nombre { get; set; }
        public string Llave { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
