import { Reporte } from '../models/reporte';

export class Evidencia {
  public id: number;
  public idReporte: number;
  public nombre: string;
  public extension: string;
  public imagen: string;
  public reporte: Reporte;

  constructor() {
    this.id = 0;
    this.idReporte = 0;
    this.nombre = '';
    this.extension = '';
    this.imagen = '';
  }
}
