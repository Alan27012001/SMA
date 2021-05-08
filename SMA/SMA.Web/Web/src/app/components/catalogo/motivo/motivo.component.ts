import { Component, OnInit } from '@angular/core';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Paginacion } from '../../../models/paginacion';
import { Ordenamiento } from '../../../models/ordenamiento';
import { Motivo } from '../../../models/motivo';

import { LoginService } from '../../../services/login.service';
import { MensajeService } from '../../../services/mensaje.service';
import { MotivoService } from '../../../services/motivo.service';

import { MotivoFormularioComponent } from '../motivo-formulario/motivo-formulario.component';

//Translate
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-motivo',
  templateUrl: './motivo.component.html',
  styleUrls: ['./motivo.component.css']
})
export class MotivoComponent implements OnInit {
  public motivos: Motivo[];
  public motivo: Motivo;
  public filtroMotivo: string;
  public filtroDescripcion: string;

  private formularioConfig: NgbModalOptions = {};
  private lastLang: string;

  public paginacion: Paginacion;
  public ordenamiento: Ordenamiento;

  constructor(
    public _loginService: LoginService,
    private _mensajeService: MensajeService,
    private _motivoService: MotivoService,
    private _modalService: NgbModal,
    private translate: TranslateService) {

    this.filtroMotivo = '';
    this.filtroDescripcion = '';

    this.formularioConfig.backdrop = 'static';
    this.formularioConfig.keyboard = false;
    this.formularioConfig.centered = true;
    this.formularioConfig.scrollable = true;
    this.formularioConfig.size = 'xl';

    this.paginacion = new Paginacion();
    this.ordenamiento = new Ordenamiento();
  }

  ngOnInit(): void {
    this._loginService.ValidarPantalla('motivo');
    this.Obtener();
  }

  //Metodos de CRUD
  Obtener() {
    this._motivoService.ObtenerMotivos(this.filtroMotivo, this.filtroDescripcion, true, this.paginacion.actual, this.paginacion.cantidad, this.ordenamiento.columna, this.ordenamiento.reversa).subscribe(
      response => {
        this.motivos = response.resultados;
        this.paginacion.Actualizar(response.paginaActual, response.paginaCantidad, response.paginaFilas, response.paginaTotal);
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }

  Agregar() {
    this.motivo = new Motivo();
    this.motivo.activo = true;
    const formulario = this._modalService.open(MotivoFormularioComponent, this.formularioConfig);
    formulario.componentInstance.motivo = this.motivo;

    formulario.result.then((reason) => {
      if (reason === 'guardado')
        this.Obtener()
    }, (respuesta) => { });
  }

  Editar(id: number) {
    this._motivoService.ObtenerMotivo(id).subscribe(
      response => {
        this.motivo = response;
        const formulario = this._modalService.open(MotivoFormularioComponent, this.formularioConfig);
        formulario.componentInstance.motivo = this.motivo;

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

  Eliminar(id: number) {
    this._motivoService.ObtenerMotivo(id).subscribe(
      response => {
        this.motivo = response;
        const eliminar = this._mensajeService.Confirmacion('¿Seguro que desea eliminar el registro de ' + this.motivo.motivo + '?');
        eliminar.result.then((resultado) => {
          if (resultado) {
            this.motivo.activo = false;
            this._motivoService.GuardarMotivo(this.motivo).subscribe(
              response => {
                this._mensajeService.Informacion(response);
                this.Obtener();
              },
              error => {
                if (!this._loginService.ManejarError(error))
                  return;
              }
            );
          }
        },
          (respuesta) => { });
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }
  //Metodos de CRUD

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
