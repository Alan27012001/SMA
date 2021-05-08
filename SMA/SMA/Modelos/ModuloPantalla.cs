using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class ModuloPantalla
    {
        public short IdModulo { get; set; }
        public short IdPantalla { get; set; }

        public virtual Modulo IdModuloNavigation { get; set; }
    }
}
