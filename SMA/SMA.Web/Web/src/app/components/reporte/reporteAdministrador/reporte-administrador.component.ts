import { Component, OnInit } from '@angular/core';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Paginacion } from '../../../models/paginacion';
import { Ordenamiento } from '../../../models/ordenamiento';
import { Reporte } from '../../../models/reporte';
import { Motivo } from '../../../models/motivo';
import { Proyecto } from '../../../models/proyecto';
import { Estatus } from '../../../models/estatus';

import { LoginService } from '../../../services/login.service';
import { MensajeService } from '../../../services/mensaje.service';
import { ReporteService } from '../../../services/reporte.service';
import { MotivoService } from '../../../services/motivo.service';
import { ProyectoService } from '../../../services/proyecto.service';
import { EstatusService } from '../../../services/estatus.service';

import { ReporteAdministradorFormularioComponent } from '../reporteAdministrador-formulario/reporte-administrador-formulario.component';
import { ReporteasignarFormularioComponent } from '../../../components/reporte/reporteAsignar/reporteasignar-formulario/reporteasignar-formulario.component';
import { ReporteterminarFormularioComponent } from '../../../components/reporte/reporteTerminar/reporteterminar-formulario/reporteterminar-formulario.component';

//Translate
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-reporte-administrador',
  templateUrl: './reporte-administrador.component.html',
  styleUrls: ['./reporte-administrador.component.css']
})
export class ReporteAdministradorComponent implements OnInit {
  public reportes: Reporte[];
  public reporte: Reporte;
  public motivos: Motivo;
  public proyectos: Proyecto;
  public estatus: Estatus;

  public filtroFolio: string;
  public filtroIdMotivo: number;
  public filtroIdProyeto: number;
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
    private _motivoService: MotivoService,
    private _proyectoService: ProyectoService,
    private _estatusService: EstatusService,
    private _modalService: NgbModal,
    private translate: TranslateService) {

    this.filtroFolio = '';
    this.filtroIdMotivo = 0;
    this.filtroIdProyeto = 0;
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
    this._loginService.ValidarPantalla('reporteadministrador');
    this.Obtener();
    this.ObtenerMotivos();
    this.ObtenerProyectos();
    this.ObtenerEstatusReporte();
  }

  //Metodos de CRUD
  Obtener() {
    this._reporteService.ObtenerReportesAdministrador(this.filtroFolio, this.filtroIdMotivo, this.filtroIdProyeto, this.filtroIdEstatusReporte, this.paginacion.actual, this.paginacion.cantidad, this.ordenamiento.columna, this.ordenamiento.reversa).subscribe(
      response => {
        this.reportes = response.resultados;
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
    const formulario = this._modalService.open(ReporteAdministradorFormularioComponent, this.formularioConfig);
    formulario.componentInstance.reporte = this.reporte;

    formulario.result.then((reason) => {
      if (reason === 'guardado')
        this.Obtener()
    }, (respuesta) => { });
  }

  Editar(id: number) {
    this._reporteService.ObtenerReporteAdministradorPorId(id).subscribe(
      response => {
        this.reporte = response;
        const formulario = this._modalService.open(ReporteAdministradorFormularioComponent, this.formularioConfig);
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

  AsignarReporte(id: number) {
    this._reporteService.ObtenerReporteAdministradorPorId(id).subscribe(
      response => {
        this.reporte = response;
        const formulario = this._modalService.open(ReporteasignarFormularioComponent, this.formularioConfig);
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

  CerrarReporte(id: number) {
    this._reporteService.ObtenerReporteAdministradorPorId(id).subscribe(
      response => {
        this.reporte = response;
        const formulario = this._modalService.open(ReporteterminarFormularioComponent, this.formularioConfig);
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
  ObtenerMotivos() {
    this._motivoService.ObtenerMotivos('', '', false, 0, 0, '', false).subscribe(
      response => {
        this.motivos = response.resultados;
      },
      error => {
        if (this._loginService.ManejarError(error))
          return;
      }
    );
  }

  ObtenerProyectos() {
    this._proyectoService.ObtenerProyectos('', '', false, 0, 0, '', false).subscribe(
      response => {
        this.proyectos = response.resultados;
      },
      error => {
        if (this._loginService.ManejarError(error))
          return;
      }
    );
  }

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
