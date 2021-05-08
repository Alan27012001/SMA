import { Component, OnInit } from '@angular/core';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Paginacion } from '../../../models/paginacion';
import { Ordenamiento } from '../../../models/ordenamiento';
import { Rol } from '../../../models/rol';

import { LoginService } from '../../../services/login.service';
import { MensajeService } from '../../../services/mensaje.service';
import { RolService } from '../../../services/rol.service';

import { RolFormularioComponent } from '../rol-formulario/rol-formulario.component';

/*Translate*/
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-rol',
  templateUrl: './rol.component.html',
  styleUrls: ['./rol.component.css']
})
export class RolComponent implements OnInit {
  public roles: Rol[];
  public rol: Rol;
  public filtroNombre: string;

  private formularioConfig: NgbModalOptions = {};

  public paginacion: Paginacion;
  public ordenamiento: Ordenamiento;

  constructor(
    public _loginService: LoginService,
    private _mensajeService: MensajeService,
    private _rolService: RolService,
    private _modalService: NgbModal,
    private translate: TranslateService
  ) {
    this.filtroNombre = '';

    this.formularioConfig.backdrop = 'static';
    this.formularioConfig.keyboard = false;
    this.formularioConfig.centered = true;
    this.formularioConfig.scrollable = true;
    this.formularioConfig.size = 'xl';

    this.paginacion = new Paginacion();
    this.ordenamiento = new Ordenamiento();
  }

  ngOnInit(): void {
    this._loginService.ValidarPantalla('rol');
    this.Obtener();
  }

  //Metodos de CRUD
  Obtener() {
    this._rolService.ObtenerRoles(this.filtroNombre, '', true, this.paginacion.actual, this.paginacion.cantidad, this.ordenamiento.columna, this.ordenamiento.reversa).subscribe(
      response => {
        this.roles = response.resultados;
        this.paginacion.Actualizar(response.paginaActual, response.paginaCantidad, response.paginaFilas, response.paginaTotal);
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }

  Agregar() {
    this.rol = new Rol();
    this.rol.activo = true;
    const formulario = this._modalService.open(RolFormularioComponent, this.formularioConfig);
    formulario.componentInstance.rol = this.rol;

    formulario.result.then((reason) => {
      if (reason === 'guardado')
        this.Obtener()
    }, (respuesta) => { });
  }

  Editar(id: number) {
    this._rolService.ObtenerRol(id).subscribe(
      response => {
        this.rol = response;
        const formulario = this._modalService.open(RolFormularioComponent, this.formularioConfig);
        formulario.componentInstance.rol = this.rol;

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
    this._rolService.ObtenerRol(id).subscribe(
      response => {
        this.rol = response;
        const eliminar = this._mensajeService.Confirmacion('¿Seguro que desea eliminar el registro de ' + this.rol.nombre + '?');
        eliminar.result.then((resultado) => {
          if (resultado) {
            this.rol.activo = false;
            this._rolService.GuardarRol(this.rol).subscribe(
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
