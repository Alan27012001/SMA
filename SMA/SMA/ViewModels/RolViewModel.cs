using System.Collections.Generic;

namespace SMA.ViewModels
{
    public class RolViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public List<MenuViewModel> Menu { get; set; }
    }
}
