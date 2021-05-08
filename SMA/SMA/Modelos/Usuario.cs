using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class Usuario
    {
        public Usuario()
        {
            InverseUsuarioCreacionNavigation = new HashSet<Usuario>();
            Reporte = new HashSet<Reporte>();
            RolUsuarioCreacionNavigation = new HashSet<Rol>();
            RolUsuarioEliminacionNavigation = new HashSet<Rol>();
            UsuarioContraseña = new HashSet<UsuarioContraseña>();
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public int? UsuarioEdicion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public string Correo { get; set; }
        public byte[] Contraseña { get; set; }
        public bool Activo { get; set; }
        public int? IdSeccion { get; set; }
        public short? IdRolApp { get; set; }
        public int? IdEstatusUsuario { get; set; }

        public virtual RolApp IdRolAppNavigation { get; set; }
        public virtual Usuario UsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Usuario> InverseUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Reporte> Reporte { get; set; }
        public virtual ICollection<Rol> RolUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Rol> RolUsuarioEliminacionNavigation { get; set; }
        public virtual ICollection<UsuarioContraseña> UsuarioContraseña { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
