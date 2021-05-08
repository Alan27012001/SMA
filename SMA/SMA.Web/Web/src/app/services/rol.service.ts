import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { MensajeService } from '../services/mensaje.service';
import { LoginService } from '../services/login.service';
import { Rol } from '../models/rol';
import { Parametro } from '../models/parametro';
import { Global } from './global';

@Injectable({ providedIn: 'root' })
export class RolService {
  public url: string;

  constructor(
    private _router: Router,
    private _mensajeService: MensajeService,
    private _loginService: LoginService,
    private _http: HttpClient
  ) {
    this.url = Global.url;
  }

  ObtenerRoles(nombre: string, descripcion: string, todos: boolean, paginacionActual: number, paginacionCantidad: number, columnaOrdenamiento: string, reversaOrdenamiento: boolean): any {
    const nombreParametro = new Parametro('nombre', nombre);
    const descripcionParametro = new Parametro('descripcion', descripcion);
    const todosParametro = new Parametro('todos', todos.toString());

    const paginacionActualParametro = new Parametro('numeroPaginacion', paginacionActual.toString());
    const paginacionCantidadParametro = new Parametro('cantidadPaginacion', paginacionCantidad.toString());
    const columnaOrdenamientoParametro = new Parametro('columnaOrdenamiento', columnaOrdenamiento);
    const reversaOrdenamientoParametro = new Parametro('reversaOrdenamiento', reversaOrdenamiento.toString());

    const parametros = [
      nombreParametro,
      descripcionParametro,
      todosParametro,
      paginacionActualParametro,
      paginacionCantidadParametro,
      columnaOrdenamientoParametro,
      reversaOrdenamientoParametro
    ];

    return this._loginService.ServicioGet('Seguridad/ObtenerRoles', parametros);
  }

  ObtenerRol(id: number): any {
    const idParametro = new Parametro('id', id.toString());

    const parametros = [idParametro];

    return this._loginService.ServicioGet('Seguridad/ObtenerRol', parametros);
  }

  GuardarRol(rol: Rol): any {
    return this._loginService.ServicioPost('Seguridad/GuardarRol', rol, true);
  }

  ObtenerMenus(): any {
    const parametros = [];
    return this._loginService.ServicioGet('Seguridad/ObtenerMenus', parametros);
  }
}
