import { Injectable } from '@angular/core';
import { Dependencia } from '../models/dependencia';
import { LoginService } from '../services/login.service';
import { Parametro } from '../models/parametro';

@Injectable({ providedIn: 'root' })

export class DependenciaService {

  constructor(
    public _loginService: LoginService
  ) { }

  ObtenerDependencias(nombre: string, clave: string, todos: boolean, paginacionActual: number, paginacionCantidad: number, columnaOrdenamiento: string, reversaOrdenamiento: boolean): any {
    const nombreParametro = new Parametro('nombre', nombre);
    const claveParametro = new Parametro('clave', clave);
    const todosParametro = new Parametro('todos', todos.toString());

    const paginacionActualParametro = new Parametro('numeroPaginacion', paginacionActual.toString());
    const paginacionCantidadParametro = new Parametro('cantidadPaginacion', paginacionCantidad.toString());
    const columnaOrdenamientoParametro = new Parametro('columnaOrdenamiento', columnaOrdenamiento);
    const reversaOrdenamientoParametro = new Parametro('reversaOrdenamiento', reversaOrdenamiento.toString());

    const parametros = [
      nombreParametro,
      claveParametro,
      todosParametro,
      paginacionActualParametro,
      paginacionCantidadParametro,
      columnaOrdenamientoParametro,
      reversaOrdenamientoParametro
    ];

    return this._loginService.ServicioGet('Catalogo/ObtenerDependencias', parametros);
  }

  ObtenerDependencia(id: number): any {
    const idParametro = new Parametro('id', id.toString());

    const parametros = [idParametro];

    return this._loginService.ServicioGet('Catalogo/ObtenerDependencia', parametros);
  }

  GuardarDependencia(dependencia: Dependencia): any {
    return this._loginService.ServicioPost('Catalogo/GuardarDependencia', dependencia, true);
  }
}
