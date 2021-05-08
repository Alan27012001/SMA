import { Permiso } from '../models/permiso';

export class Pantalla {
  public idPantalla: number;
  public pantalla: string;
  public llave: string;
  public permisos: Permiso[]

  constructor() {
    this.permisos = [];
  }
}
