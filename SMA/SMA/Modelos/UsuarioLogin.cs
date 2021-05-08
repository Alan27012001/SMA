using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class UsuarioLogin
    {
        public Guid Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVigencia { get; set; }
        public bool Activo { get; set; }
    }
}
