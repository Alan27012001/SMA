import { Pantalla } from '../models/pantalla';

export class Menu {
  public idModulo: number;
  public modulo: string;
  public llave: string;
  public pantallas: Pantalla[]

  constructor() {
    this.pantallas = [];
  }

}
