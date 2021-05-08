import { Injectable } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Reporte } from '../models/reporte';
import { Parametro } from '../models/parametro';
import { Global } from './global';

@Injectable({ providedIn: 'root' })
export class ReporteService {
  public url: string;

  constructor(
    private _loginService: LoginService
  ) {
    this.url = Global.url;
  }

  //Reportes del Administrador
  ObtenerReportesAdministrador(folio: string, motivo: number, proyecto: number, estatusreporte: number, paginacionActual: number, paginacionCantidad: number, columnaOrdenamiento: string, reversaOrdenamiento: boolean): any {
    const folioParametro = new Parametro('folio', folio);
    const motivoParametro = new Parametro('idMotivo', motivo === null || motivo === undefined ? '' : motivo.toString());
    const proyectoParametro = new Parametro('idProyecto', proyecto === null || proyecto === undefined ? '' : proyecto.toString());
    const estatusReporteParametro = new Parametro('idEstatusReporte', estatusreporte === null || estatusreporte === undefined ? '' : estatusreporte.toString());

    const paginacionActualParametro = new Parametro('numeroPaginacion', paginacionActual.toString());
    const paginacionCantidadParametro = new Parametro('cantidadPaginacion', paginacionCantidad.toString());
    const columnaOrdenamientoParametro = new Parametro('columnaOrdenamiento', columnaOrdenamiento);
    const reversaOrdenamientoParametro = new Parametro('reversaOrdenamiento', reversaOrdenamiento.toString());

    const parametros = [
      folioParametro,
      motivoParametro,
      proyectoParametro,
      estatusReporteParametro,
      paginacionActualParametro,
      paginacionCantidadParametro,
      columnaOrdenamientoParametro,
      reversaOrdenamientoParametro
    ];

    return this._loginService.ServicioGet('Reporte/ObtenerReportesAdministrador', parametros);
  }

  ObtenerReporteAdministradorPorId(id: number): any {
    const idParametro = new Parametro('id', id.toString());

    const parametros = [idParametro];

    return this._loginService.ServicioGet('Reporte/ObtenerReporteAdministradorPorId', parametros);
  }

  GuardarReporteAdminstrador(reporte: Reporte): any {
    return this._loginService.ServicioPost('Reporte/GuardarReporte', reporte, true);
  }

  AsignarReporte(reporte: Reporte): any {
    return this._loginService.ServicioPost('Reporte/AsignarReporte', reporte, true);
  }

  CerrarReporte(reporte: Reporte): any {
    return this._loginService.ServicioPost('Reporte/CerrarReporte', reporte, true);
  }
   //Reportes del Administrador

  //Reportes del Usuario
  ObtenerReportesUsuario(folio: string, idestatusreporte: number, paginacionActual: number, paginacionCantidad: number, columnaOrdenamiento: string, reversaOrdenamiento: boolean): any {
    const folioParametro = new Parametro('folio', folio);
    const estatusReporteParametro = new Parametro('estatusReporte', idestatusreporte === null || idestatusreporte === undefined ? '' : idestatusreporte.toString());

    const paginacionActualParametro = new Parametro('numeroPaginacion', paginacionActual.toString());
    const paginacionCantidadParametro = new Parametro('cantidadPaginacion', paginacionCantidad.toString());
    const columnaOrdenamientoParametro = new Parametro('columnaOrdenamiento', columnaOrdenamiento);
    const reversaOrdenamientoParametro = new Parametro('reversaOrdenamiento', reversaOrdenamiento.toString());

    const parametros = [
      folioParametro,
      estatusReporteParametro,
      paginacionActualParametro,
      paginacionCantidadParametro,
      columnaOrdenamientoParametro,
      reversaOrdenamientoParametro
    ];

    return this._loginService.ServicioGet('Reporte/ObtenerReportesUsuario', parametros);
  }

  ObtenerReporteUsuarioPorId(id: number): any {
    const idParametro = new Parametro('id', id.toString());

    const parametros = [idParametro];

    return this._loginService.ServicioGet('Reporte/ObtenerReporteUsuarioPorId', parametros);
  }

  GuardarReporteUsuario(reporte: Reporte): any {
    return this._loginService.ServicioPost('Reporte/GuardarReporte', reporte, true);
  }
  //Reportes del Usuario
}
