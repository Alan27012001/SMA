import { Rol } from '../models/rol';

export class Usuario {
  public id: number;
  public nombre: string;
  public apellidoPaterno: string;
  public apellidoMaterno: string;
  public correo: string;
  public contraseña: string;
  public contrasena: string;
  public confirmarContrasena: string;
  public contraseñaCadena: string;
  public activo: boolean;
  public fechaNacimiento: Date;
  public rol: Rol[];
  public recuperar: string;

  constructor() {
    this.id = 0;
    this.nombre = '';
    this.apellidoPaterno = '';
    this.apellidoMaterno = '';
    this.correo = '';
    this.contrasena = '';
    this.confirmarContrasena = '';
  }
}
