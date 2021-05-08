using System;
using System.Collections.Generic;

namespace SMA.Modelos
{
    public partial class Reporte
    {
        public Reporte()
        {
            Evidencia = new HashSet<Evidencia>();
        }

        public int Id { get; set; }
        public string Folio { get; set; }
        public int? IdMotivo { get; set; }
        public int? IdProyecto { get; set; }
        public DateTime? FechaReporte { get; set; }
        public string ComentarioReporte { get; set; }
        public int? IdEstatusReporte { get; set; }
        public int? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? UsuarioEdicion { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public int? UsuarioElimincion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuarioAsignacion { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public string ComentarioAsignacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public string ComentarioFinalizacion { get; set; }

        public virtual Estatus IdEstatusReporteNavigation { get; set; }
        public virtual Motivo IdMotivoNavigation { get; set; }
        public virtual Proyecto IdProyectoNavigation { get; set; }
        public virtual Usuario IdUsuarioAsignacionNavigation { get; set; }
        public virtual ICollection<Evidencia> Evidencia { get; set; }
    }
}
