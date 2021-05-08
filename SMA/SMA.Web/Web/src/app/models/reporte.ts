import { Estatus } from '../models/estatus';
import { Proyecto } from '../models/proyecto';
import { Motivo } from '../models/motivo';
import { Usuario } from '../models/usuario';

export class Reporte {
  public id: number;
  public folio: string;
  public idMotivo: number;
  public idProyecto: number;
  public idUsuario: number;
  public fechaReporte: Date;
  public comentarioReporte: string;
  public idEstatusReporte: number;
  public idUsuarioAsignacion: number;
  public fechaAsignacion: Date;
  public comentarioAsignacion: string;
  public fechaFinalizacion: Date;
  public comentarioFinalizacion: string;
  public proyecto: Proyecto;
  public motivo: Motivo;
  public estatus: Estatus;
  public usuario: Usuario;

  constructor() {
    this.id = 0;
    this.folio = '';
    this.idMotivo = 0;
    this.idProyecto = 0;
    this.idEstatusReporte = 0;
    this.idUsuarioAsignacion = 0;
    this.comentarioReporte = '';
    this.comentarioAsignacion = '';
    this.comentarioFinalizacion = '';
  }
}
