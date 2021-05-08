import { Component, OnInit } from '@angular/core';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Paginacion } from '../../../models/paginacion';
import { Ordenamiento } from '../../../models/ordenamiento';
import { Usuario } from '../../../models/usuario';

import { LoginService } from '../../../services/login.service';
import { MensajeService } from '../../../services/mensaje.service';
import { UsuarioService } from '../../../services/usuario.service';

import { UsuarioFormularioComponent } from '../usuario-formulario/usuario-formulario.component';

//Translate
import { TranslateService } from '@ngx-translate/core'

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent implements OnInit {
  public usuarios: Usuario[];
  public usuario: Usuario;
  public filtroNombre: string;
  public filtroCorreo: string;

  private formularioConfig: NgbModalOptions = {};
  private lastLang: string;

  public paginacion: Paginacion;
  public ordenamiento: Ordenamiento;

  constructor(
    public _loginService: LoginService,
    private _mensajeService: MensajeService,
    private _usuarioService: UsuarioService,
    private _modalService: NgbModal,
    private translate: TranslateService) {
    
    this.filtroNombre = '';
    this.filtroCorreo = '';

    this.formularioConfig.backdrop = 'static';
    this.formularioConfig.keyboard = false;
    this.formularioConfig.centered = true;
    this.formularioConfig.scrollable = true;
    this.formularioConfig.size = 'xl';

    this.paginacion = new Paginacion();
    this.ordenamiento = new Ordenamiento();
  }

  ngOnInit(): void {
    this._loginService.ValidarPantalla('usuario');
    this.Obtener();
  }

  //Metodos de CRUD
  Obtener() {
    this._usuarioService.ObtenerUsuarios(this.filtroNombre, this.filtroCorreo, true, this.paginacion.actual, this.paginacion.cantidad, this.ordenamiento.columna, this.ordenamiento.reversa).subscribe(
      response => {
        this.usuarios = response.resultados;
        this.paginacion.Actualizar(response.paginaActual, response.paginaCantidad, response.paginaFilas, response.paginaTotal);
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }

  Agregar() {
    this.usuario = new Usuario();
    this.usuario.activo = true;
    const formulario = this._modalService.open(UsuarioFormularioComponent, this.formularioConfig);
    formulario.componentInstance.usuario = this.usuario;

    formulario.result.then((reason) => {
      if (reason === 'guardado')
        this.Obtener()
    }, (respuesta) => { });
  }

  Editar(id: number) {
    this._usuarioService.ObtenerUsuario(id).subscribe(
      response => {
        this.usuario = response;
        const formulario = this._modalService.open(UsuarioFormularioComponent, this.formularioConfig);
        formulario.componentInstance.usuario = this.usuario;

        formulario.result.then((reason) => {
          if (reason === 'guardado')
            this.Obtener()
        }, (respuesta) => {});
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }

  Eliminar(id: number) {
    this._usuarioService.ObtenerUsuario(id).subscribe(
      response => {
        this.usuario = response;
        const eliminar = this._mensajeService.Confirmacion('¿Seguro que desea eliminar el registro de ' + this.usuario.nombre + ' ' + this.usuario.apellidoPaterno + '?');
        eliminar.result.then((resultado) => {
          if (resultado) {
            this.usuario.activo = false;
            this._usuarioService.GuardarUsuario(this.usuario).subscribe(
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
          (respuesta) => {});
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
