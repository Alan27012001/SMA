import { Component, OnInit } from '@angular/core';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Paginacion } from '../../../models/paginacion';
import { Ordenamiento } from '../../../models/ordenamiento';
import { Proyecto } from '../../../models/proyecto';

import { LoginService } from '../../../services/login.service';
import { MensajeService } from '../../../services/mensaje.service';
import { ProyectoService } from '../../../services/proyecto.service';

import { ProyectoFormularioComponent } from '../proyecto-formulario/proyecto-formulario.component';

//Translate
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-proyecto',
  templateUrl: './proyecto.component.html',
  styleUrls: ['./proyecto.component.css']
})
export class ProyectoComponent implements OnInit {
  public proyectos: Proyecto[];
  public proyecto: Proyecto;
  public filtroNombre: string;
  public filtroDescripcion: string;

  private formularioConfig: NgbModalOptions = {};
  private lastLang: string;

  public paginacion: Paginacion;
  public ordenamiento: Ordenamiento;

  constructor(
    public _loginService: LoginService,
    private _mensajeService: MensajeService,
    private _proyectoService: ProyectoService,
    private _modalService: NgbModal,
    private translate: TranslateService) {

    this.filtroNombre = '';
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
    this._loginService.ValidarPantalla('proyecto');
    this.Obtener();
  }

  //Metodos de CRUD
  Obtener() {
    this._proyectoService.ObtenerProyectos(this.filtroNombre, this.filtroDescripcion, true, this.paginacion.actual, this.paginacion.cantidad, this.ordenamiento.columna, this.ordenamiento.reversa).subscribe(
      response => {
        this.proyectos = response.resultados;
        this.paginacion.Actualizar(response.paginaActual, response.paginaCantidad, response.paginaFilas, response.paginaTotal);
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }

  Agregar() {
    this.proyecto = new Proyecto();
    this.proyecto.activo = true;
    const formulario = this._modalService.open(ProyectoFormularioComponent, this.formularioConfig);
    formulario.componentInstance.proyecto = this.proyecto;

    formulario.result.then((reason) => {
      if (reason === 'guardado')
        this.Obtener()
    }, (respuesta) => { });
  }

  Editar(id: number) {
    this._proyectoService.ObtenerProyecto(id).subscribe(
      response => {
        this.proyecto = response;
        const formulario = this._modalService.open(ProyectoFormularioComponent, this.formularioConfig);
        formulario.componentInstance.proyecto = this.proyecto;

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
    this._proyectoService.ObtenerProyecto(id).subscribe(
      response => {
        this.proyecto = response;
        const eliminar = this._mensajeService.Confirmacion('¿Seguro que desea eliminar el registro de ' + this.proyecto.nombre + '?');
        eliminar.result.then((resultado) => {
          if (resultado) {
            this.proyecto.activo = false;
            this._proyectoService.GuardarProyecto(this.proyecto).subscribe(
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
