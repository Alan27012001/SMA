import { Injectable } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Proyecto } from '../models/proyecto';
import { Parametro } from '../models/parametro';
import { Global } from './global';

@Injectable({ providedIn: 'root' })
export class ProyectoService {
  public url: string;

  constructor(
    private _loginService: LoginService
  ) {
    this.url = Global.url;
  }

  ObtenerProyectos(nombre: string, descripcion: string, todos: boolean, paginacionActual: number, paginacionCantidad: number, columnaOrdenamiento: string, reversaOrdenamiento: boolean): any {
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

    return this._loginService.ServicioGet('Catalogo/ObtenerProyectos', parametros);
  }

  ObtenerProyecto(id: number): any {
    const idParametro = new Parametro('id', id.toString());

    const parametros = [idParametro];

    return this._loginService.ServicioGet('Catalogo/ObtenerProyecto', parametros);
  }

  GuardarProyecto(proyecto: Proyecto): any {
    return this._loginService.ServicioPost('Catalogo/GuardarProyecto', proyecto, true);
  }
}
