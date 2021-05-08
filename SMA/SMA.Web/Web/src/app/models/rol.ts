import { Menu } from '../models/menu';

export class Rol {
  public id: number;
  public nombre: string;
  public descripcion: string;
  public activo: boolean;
  public menu: Menu[];

  constructor() {
    this.id = 0;
    this.nombre = '';
    this.descripcion = '';
    this.activo = true;
  }
}
