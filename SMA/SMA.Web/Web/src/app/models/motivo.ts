export class Motivo {
  public id: number;
  public motivo: string;
  public descripcion: string;
  public activo: boolean;

  constructor() {
    this.id = 0;
    this.motivo = '';
    this.descripcion = '';
  }
}
