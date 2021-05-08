using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMA.ViewModels
{
    public class EvidenciaViewModel
    {
        public int Id { get; set; }
        public int IdReporte { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public string Imagen { get; set; }
    }
}
