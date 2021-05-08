using System;
using System.Collections.Generic;

namespace SMA.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ContraseñaCadena { get; set; }
        public string Correo { get; set; }

        public bool Activo { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public List<RolViewModel> Rol { get; set; }
    }
}
