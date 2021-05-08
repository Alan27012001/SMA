import { Injectable } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Estatus } from '../models/estatus';
import { Parametro } from '../models/parametro';
import { Global } from './global';

@Injectable({ providedIn: 'root' })
export class EstatusService {
  public url: string;

  constructor(
    private _loginService: LoginService
  ) {
    this.url = Global.url;
  }

  ObtenerEstatusReporte(nombre: string, paginacionActual: number, paginacionCantidad: number, columnaOrdenamiento: string, reversaOrdenamiento: boolean): any {
    const nombreParametro = new Parametro('nombre', nombre);

    const paginacionActualParametro = new Parametro('numeroPaginacion', paginacionActual.toString());
    const paginacionCantidadParametro = new Parametro('cantidadPaginacion', paginacionCantidad.toString());
    const columnaOrdenamientoParametro = new Parametro('columnaOrdenamiento', columnaOrdenamiento);
    const reversaOrdenamientoParametro = new Parametro('reversaOrdenamiento', reversaOrdenamiento.toString());

    const parametros = [
      nombreParametro,
      paginacionActualParametro,
      paginacionCantidadParametro,
      columnaOrdenamientoParametro,
      reversaOrdenamientoParametro
    ];

    return this._loginService.ServicioGet('Configuracion/ObtenerEstatusReporte', parametros);
  }
}
