using SMA.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMA.ViewModels
{
    public class ReporteViewModel
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public int? IdMotivo { get; set; }
        public int? IdProyecto { get; set; }
        public DateTime? FechaReporte { get; set; }
        public string ComentarioReporte { get; set; }
        public int? IdEstatusReporte { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioEdicion { get; set; }
        public DateTime FechaEdicion { get; set; }
        public int UsuarioElimincion { get; set; }
        public DateTime FechaEliminacion { get; set; }
        public int? IdUsuarioAsignacion { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public string ComentarioAsignacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public string ComentarioFinalizacion { get; set; }
        public ProyectoViewModel Proyecto { get; set; }
        public MotivoViewModel Motivo { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public EstatusViewModel Estatus { get; set; }
    }
}
