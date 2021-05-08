import { Injectable } from '@angular/core';
import { LoginService } from './login.service';
import { Usuario } from '../models/usuario';
import { Parametro } from '../models/parametro';
import { Global } from './global';

@Injectable({ providedIn: 'root' })
export class RestablecerContrasenaService {
  public url: string;

  constructor(
    private _loginService: LoginService
  ) {
    this.url = Global.url;
  }

  ValidarId(id: string): any {
    const idParametro = new Parametro('id', id);

    const parametros = [ idParametro ];

    return this._loginService.ServicioGetSinToken('Seguridad/RestablecerContrasena', parametros);
  }

  ActualizarContrasena(usuario: Usuario): any {
    return this._loginService.ServicioPostSinToken('Seguridad/ActualizarContrasena', usuario, true);
  }
}
