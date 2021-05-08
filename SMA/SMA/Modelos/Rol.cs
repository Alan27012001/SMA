using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class Rol
    {
        public Rol()
        {
            RolModuloPantallaPermiso = new HashSet<RolModuloPantallaPermiso>();
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public short Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public int? UsuarioEdicion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public bool? Activo { get; set; }

        public virtual Usuario UsuarioCreacionNavigation { get; set; }
        public virtual Usuario UsuarioEliminacionNavigation { get; set; }
        public virtual ICollection<RolModuloPantallaPermiso> RolModuloPantallaPermiso { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
