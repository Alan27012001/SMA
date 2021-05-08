using System.Collections.Generic;

namespace SMA.ViewModels
{
    public class MenuViewModel
    {
        public int IdModulo { get; set; }
        public string Modulo { get; set; }
        public string Llave { get; set; }
        public List<PantallaViewModel> Pantallas { get; set; }
    }

    public class PantallaViewModel
    {
        public int IdPantalla { get; set; }
        public string Pantalla { get; set; }
        public string Llave { get; set; }
        public List<PermisoViewModel> Permisos { get; set; }
    }

    public class PermisoViewModel
    {
        public int IdPermiso { get; set; }
        public string Permiso { get; set; }
        public string Descripcion { get; set; }
        public string Llave { get; set; }
    }
}
