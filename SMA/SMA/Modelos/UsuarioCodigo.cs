using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class UsuarioCodigo
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoCodigo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Codigo { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaVigencia { get; set; }
    }
}
