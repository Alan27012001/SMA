using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMA.ViewModels
{
    public class MotivoViewModel
    {
        public int Id { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
    }
}
