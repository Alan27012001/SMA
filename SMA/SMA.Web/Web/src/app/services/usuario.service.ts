import { Injectable } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Usuario } from '../models/usuario';
import { Parametro } from '../models/parametro';
import { Global } from './global';

@Injectable({ providedIn: 'root' })
export class UsuarioService {
  public url: string;
  
  constructor(
    private _loginService: LoginService
  ) {
    this.url = Global.url;
  }

  ObtenerUsuarios(nombre: string, correo: string, todos: boolean, paginacionActual: number, paginacionCantidad: number, columnaOrdenamiento: string, reversaOrdenamiento: boolean): any {
    const nombreParametro = new Parametro('nombre', nombre);
    const correoParametro = new Parametro('correo', correo);
    const todosParametro = new Parametro('todos', todos.toString());

    const paginacionActualParametro = new Parametro('numeroPaginacion', paginacionActual.toString());
    const paginacionCantidadParametro = new Parametro('cantidadPaginacion', paginacionCantidad.toString());
    const columnaOrdenamientoParametro = new Parametro('columnaOrdenamiento', columnaOrdenamiento);
    const reversaOrdenamientoParametro = new Parametro('reversaOrdenamiento', reversaOrdenamiento.toString());

    const parametros = [
      nombreParametro,
      correoParametro,
      todosParametro,
      paginacionActualParametro,
      paginacionCantidadParametro,
      columnaOrdenamientoParametro,
      reversaOrdenamientoParametro
    ];

    return this._loginService.ServicioGet('Seguridad/ObtenerUsuarios', parametros);
  }

  ObtenerUsuario(id: number): any {
    const idParametro = new Parametro('id', id.toString());

    const parametros = [ idParametro ];

    return this._loginService.ServicioGet('Seguridad/ObtenerUsuario', parametros);
  }

  GuardarUsuario(usuario: Usuario): any {
    return this._loginService.ServicioPost('Seguridad/GuardarUsuario', usuario, true);
  }
}
