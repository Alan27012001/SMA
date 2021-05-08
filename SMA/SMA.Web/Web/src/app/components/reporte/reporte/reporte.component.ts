import { Component, OnInit } from '@angular/core';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Paginacion } from '../../../models/paginacion';
import { Ordenamiento } from '../../../models/ordenamiento';
import { Reporte } from '../../../models/reporte';
import { Estatus } from '../../../models/estatus';

import { LoginService } from '../../../services/login.service';
import { MensajeService } from '../../../services/mensaje.service';
import { ReporteService } from '../../../services/reporte.service';
import { EstatusService } from '../../../services/estatus.service';

import { ReporteFormularioComponent } from '../reporte-formulario/reporte-formulario.component';
import { ReporteusuarioterminarFormularioComponent } from '../../../components/reporte/reporteUsuarioTerminar/reporteusuarioterminar-formulario/reporteusuarioterminar-formulario.component';

//Translate
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-reporte',
  templateUrl: './reporte.component.html',
  styleUrls: ['./reporte.component.css']
})
export class ReporteComponent implements OnInit {
  public reportes: Reporte[];
  public reporte: Reporte;
  public estatus: Estatus;

  public filtroFolio: string;
  public filtroIdEstatusReporte: number;
  public ccsUrl: string;

  private formularioConfig: NgbModalOptions = {};
  private lastLang: string;

  public paginacion: Paginacion;
  public ordenamiento: Ordenamiento;

  constructor(
    public _loginService: LoginService,
    private _mensajeService: MensajeService,
    private _reporteService: ReporteService,
    private _estatusService: EstatusService,
    private _modalService: NgbModal,
    private translate: TranslateService) {

    this.filtroFolio = '';
    this.filtroIdEstatusReporte = 0;

    this.formularioConfig.backdrop = 'static';
    this.formularioConfig.keyboard = false;
    this.formularioConfig.centered = true;
    this.formularioConfig.scrollable = true;
    this.formularioConfig.size = 'xl';

    this.paginacion = new Paginacion();
    this.ordenamiento = new Ordenamiento();
  }

  ngOnInit(): void {
    this._loginService.ValidarPantalla('reporte');
    this.Obtener();
    this.ObtenerEstatusReporte();
  }

  //Metodos de CRUD
  Obtener() {
    this._reporteService.ObtenerReportesUsuario(this.filtroFolio, this.filtroIdEstatusReporte, this.paginacion.actual, this.paginacion.cantidad, this.ordenamiento.columna, this.ordenamiento.reversa).subscribe(
      response => {
        this.reportes = response.resultados;
        console.log(response);
        this.paginacion.Actualizar(response.paginaActual, response.paginaCantidad, response.paginaFilas, response.paginaTotal);
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }

  Agregar() {
    this.reporte = new Reporte();
    const formulario = this._modalService.open(ReporteFormularioComponent, this.formularioConfig);
    formulario.componentInstance.reporte = this.reporte;

    formulario.result.then((reason) => {
      if (reason === 'guardado')
        this.Obtener()
    }, (respuesta) => { });
  }

  Editar(id: number) {
    this._reporteService.ObtenerReporteUsuarioPorId(id).subscribe(
      response => {
        this.reporte = response;
        const formulario = this._modalService.open(ReporteusuarioterminarFormularioComponent, this.formularioConfig);
        formulario.componentInstance.reporte = this.reporte;

        formulario.result.then((reason) => {
          if (reason === 'guardado')
            this.Obtener()
        }, (respuesta) => { });
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }
  //Metodos de CRUD

  //Metodos del combo
  ObtenerEstatusReporte() {
    this._estatusService.ObtenerEstatusReporte('', 0, 0, '', false).subscribe(
      response => {
        this.estatus = response.resultados;
        console.log(response);
      },
      error => {
        if (this._loginService.ManejarError(error))
          return;
      }
    );
  }

  //Metodos del combo

  //Funcionalidad Tabla
  Ordenar(columna: string, reversa: boolean) {
    this.ordenamiento.columna = columna;
    this.ordenamiento.reversa = reversa;
    this.Obtener();
  }

  PaginacionIr(pagina: number) {
    this.paginacion.Ir(pagina);
    this.Obtener();
  }

  PaginacionSiguiente() {
    this.paginacion.Siguiente();
    this.Obtener();
  }

  PaginacionAnterior() {
    this.paginacion.Anterior();
    this.Obtener();
  }
  //Funcionalidad Tabla

}
