import { Injectable } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Motivo } from '../models/motivo';
import { Parametro } from '../models/parametro';
import { Global } from './global';

@Injectable({ providedIn: 'root' })
export class MotivoService {
  public url: string;

  constructor(
    private _loginService: LoginService
  ) {
    this.url = Global.url;
  }

  ObtenerMotivos(motivo: string, descripcion: string, todos: boolean, paginacionActual: number, paginacionCantidad: number, columnaOrdenamiento: string, reversaOrdenamiento: boolean): any {
    const motivoParametro = new Parametro('motivo', motivo);
    const descripcionParametro = new Parametro('descripcion', descripcion);
    const todosParametro = new Parametro('todos', todos.toString());

    const paginacionActualParametro = new Parametro('numeroPaginacion', paginacionActual.toString());
    const paginacionCantidadParametro = new Parametro('cantidadPaginacion', paginacionCantidad.toString());
    const columnaOrdenamientoParametro = new Parametro('columnaOrdenamiento', columnaOrdenamiento);
    const reversaOrdenamientoParametro = new Parametro('reversaOrdenamiento', reversaOrdenamiento.toString());

    const parametros = [
      motivoParametro,
      descripcionParametro,
      todosParametro,
      paginacionActualParametro,
      paginacionCantidadParametro,
      columnaOrdenamientoParametro,
      reversaOrdenamientoParametro
    ];

    return this._loginService.ServicioGet('Catalogo/ObtenerMotivos', parametros);
  }

  ObtenerMotivo(id: number): any {
    const idParametro = new Parametro('id', id.toString());

    const parametros = [idParametro];

    return this._loginService.ServicioGet('Catalogo/ObtenerMotivo', parametros);
  }

  GuardarMotivo(motivo: Motivo): any {
    return this._loginService.ServicioPost('Catalogo/GuardarMotivo', motivo, true);
  }
}
