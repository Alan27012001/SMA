import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Menu } from '../../../models/menu';
import { Pantalla } from '../../../models/pantalla';
import { Permiso } from '../../../models/permiso';
import { Rol } from '../../../models/rol';

import { LoginService } from '../../../services/login.service';
import { MensajeService } from '../../../services/mensaje.service';

import { RolService } from '../../../services/rol.service';

//Translate
import { TranslateService } from '@ngx-translate/core'

@Component({
  selector: 'app-rol-formulario',
  templateUrl: './rol-formulario.component.html',
  styleUrls: ['./rol-formulario.component.css']
})
export class RolFormularioComponent implements OnInit {
  public rol: Rol;
  public rolForm: FormGroup;
  public menus: Menu[];

  constructor(
    public activeModal: NgbActiveModal,
    public _loginService: LoginService,
    private _mensajeService: MensajeService,
    private _rolService: RolService,
    private translate: TranslateService
  ) {

  }

  ngOnInit(): void {
    this.CrearForm();
    this.ObtenerMenus();
  }

  Aceptar() {
    this.rol.nombre = this.rolForm.value.nombre;
    this.rol.descripcion = this.rolForm.value.descripcion;
    this.rol.activo = this.rolForm.value.activo;
    this.rol.menu = [];

    for (let i = 0; i < this.menus.length; i++) {
      for (let y = 0; y < this.menus[i].pantallas.length; y++) {
        for (let z = 0; z < this.menus[i].pantallas[y].permisos.length; z++) {
          const control = this.menus[i].idModulo + '_' + this.menus[i].pantallas[y].idPantalla + '_' + this.menus[i].pantallas[y].permisos[z].idPermiso;
          const valor = this.rolForm.get(control).value;

          if (valor) {
            let existe = false;

            let indexModulo = this.rol.menu.length;
            for (let x = 0; x < this.rol.menu.length; x++) {
              if (this.rol.menu[x].idModulo === this.menus[i].idModulo) {
                existe = true;
                indexModulo = x;
              }
            }

            if (!existe) {
              const modulo = new Menu();
              modulo.idModulo = this.menus[i].idModulo;
              modulo.modulo = this.menus[i].modulo;
              this.rol.menu.push(modulo);
            }

            existe = false;
            let indexPantalla = this.rol.menu[indexModulo].pantallas.length;
            for (let x = 0; x < this.rol.menu[indexModulo].pantallas.length; x++) {
              if (this.rol.menu[indexModulo].pantallas[x].idPantalla === this.menus[i].pantallas[y].idPantalla) {
                existe = true;
                indexPantalla = x;
              }
            }

            if (!existe) {
              const pantalla = new Pantalla();
              pantalla.idPantalla = this.menus[i].pantallas[y].idPantalla;
              pantalla.pantalla = this.menus[i].pantallas[y].pantalla;
              this.rol.menu[indexModulo].pantallas.push(pantalla);
            }

            existe = false;
            for (let x = 0; x < this.rol.menu[indexModulo].pantallas[indexPantalla].permisos.length; x++)
              if (this.rol.menu[indexModulo].pantallas[indexPantalla].permisos[x].idPermiso === this.menus[i].pantallas[y].permisos[z].idPermiso)
                existe = true;

            if (!existe) {
              const permiso = new Permiso();
              permiso.idPermiso = this.menus[i].pantallas[y].permisos[z].idPermiso;
              permiso.permiso = this.menus[i].pantallas[y].permisos[z].permiso;
              this.rol.menu[indexModulo].pantallas[indexPantalla].permisos.push(permiso);
            }
          }
        }
      }
    }

    console.log(this.rol.menu);
    
    this.Guardar();
  }

  Cancelar() {
    this.activeModal.close('Close click');
  }

  //Metodos Formulario
  CrearForm() {
    this.rolForm = new FormGroup({
      nombre: new FormControl(this.rol.nombre, [
        Validators.required,
        Validators.maxLength(50)
      ]),
      descripcion: new FormControl(this.rol.descripcion, [
        Validators.maxLength(250)
      ]),
      activo: new FormControl(this.rol.activo, [
        Validators.required
      ])
    });
  }

  CargarMenus() {
    for (let i = 0; i < this.menus.length; i++) {
      for (let y = 0; y < this.menus[i].pantallas.length; y++) {
        for (let z = 0; z < this.menus[i].pantallas[y].permisos.length; z++) {
          this.rolForm.addControl(this.menus[i].idModulo + '_' + this.menus[i].pantallas[y].idPantalla + '_' + this.menus[i].pantallas[y].permisos[z].idPermiso, new FormControl(false));
        }
      }
    }

    if (this.rol.menu !== null && this.rol.menu !== undefined) {
      for (let i = 0; i < this.rol.menu.length; i++) {
        for (let y = 0; y < this.rol.menu[i].pantallas.length; y++) {
          for (let z = 0; z < this.rol.menu[i].pantallas[y].permisos.length; z++) {
            const nombre = this.rol.menu[i].idModulo + '_' + this.rol.menu[i].pantallas[y].idPantalla + '_' + this.rol.menu[i].pantallas[y].permisos[z].idPermiso;
            this.rolForm.controls[nombre].setValue(true);
          }
        }
      }
    }

    if (!this._loginService.escritura)
      this.rolForm.disable();
  }
  //Metodos Formulario

  //Metodos Servicios
  Guardar() {
    this._rolService.GuardarRol(this.rol).subscribe(
      response => {
        this._mensajeService.Informacion(response);
        this.activeModal.close('guardado');
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }

  ObtenerMenus() {
    this._rolService.ObtenerMenus().subscribe(
      response => {
        this.menus = response;
        this.CargarMenus();
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }
  //Metodos Servicios

}
