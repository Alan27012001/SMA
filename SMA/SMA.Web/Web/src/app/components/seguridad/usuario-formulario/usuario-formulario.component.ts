import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';

import { Usuario } from '../../../models/usuario';
import { Rol } from '../../../models/rol';

import { LoginService } from '../../../services/login.service';
import { MensajeService } from '../../../services/mensaje.service';
import { ConstanteService } from '../../../services/constante.service';

//Translate
import { TranslateService } from '@ngx-translate/core'

import { UsuarioService } from '../../../services/usuario.service';
import { RolService } from '../../../services/rol.service';

@Component({
  selector: 'app-usuario-formulario',
  templateUrl: './usuario-formulario.component.html',
  styleUrls: ['./usuario-formulario.component.css']
})
export class UsuarioFormularioComponent implements OnInit {
  public usuario: Usuario;
  public usuarioForm: FormGroup;
  public roles: Rol[];

  constructor(
    public activeModal: NgbActiveModal,
    public fechas: DatePipe,
    public _loginService: LoginService,
    private _mensajeService: MensajeService,
    private _constanteService: ConstanteService,
    private _usuarioService: UsuarioService,
    private _rolService: RolService,
    private translate: TranslateService) {
  }

  ngOnInit(): void {
    this._loginService.ValidarPantalla('usuario');
    this.CrearForm();
    this.ObtenerRoles();
  }

  Aceptar() {
    this.usuario.nombre = this.usuarioForm.value.nombre;
    this.usuario.apellidoPaterno = this.usuarioForm.value.apellidoPaterno;
    this.usuario.apellidoMaterno = this.usuarioForm.value.apellidoMaterno;
    this.usuario.correo = this.usuarioForm.value.correo;
    this.usuario.fechaNacimiento = this.usuarioForm.value.fechaNacimiento;
    this.usuario.contraseñaCadena = this.usuarioForm.value.contrasena;
    this.usuario.activo = this.usuarioForm.value.activo;
    this.usuario.rol = [];
    for (let i = 0; i < this.usuarioForm.value.rol.length; i++) {
      const rol = new Rol();
      rol.id = this.usuarioForm.value.rol[i];
      this.usuario.rol.push(rol);
    }
    this.Guardar();
  }

  Cancelar() {
    this.activeModal.close('Close click');
  }

  //Metodos Formulario
  CrearForm() {
    const idRoles = [];
    let contrasenaValidators = [Validators.required, Validators.maxLength(50)];
    let confirmarContrasenaValidators = [Validators.required, Validators.maxLength(50)];

    if (this.usuario.id !== null && this.usuario.id !== undefined) {
      if (this.usuario.id > 0) {
        contrasenaValidators = [Validators.maxLength(50)];
        confirmarContrasenaValidators = [Validators.maxLength(50)];
      }
    }

    if (this.usuario.rol !== null && this.usuario.rol !== undefined)
      for (let i = 0; i < this.usuario.rol.length; i++)
        idRoles.push(this.usuario.rol[i].id);
     
    this.usuarioForm = new FormGroup({
      nombre: new FormControl(this.usuario.nombre,
      [
        Validators.required,
        Validators.maxLength(80),
        Validators.pattern(this._constanteService.PatternLetras)
      ]),
      apellidoPaterno: new FormControl(this.usuario.apellidoPaterno, [
        Validators.required,
        Validators.maxLength(80),
        Validators.pattern(this._constanteService.PatternLetras)
      ]),
      apellidoMaterno: new FormControl(this.usuario.apellidoMaterno, [
        Validators.required,
        Validators.maxLength(80),
        Validators.pattern(this._constanteService.PatternLetras)
      ]),
      correo: new FormControl(this.usuario.correo, [
        Validators.required,
        Validators.email,
        Validators.maxLength(80)
      ]),
      fechaNacimiento: new FormControl(this.fechas.transform(this.usuario.fechaNacimiento, 'yyyy-MM-dd'), [
        Validators.required,
        this._loginService.FechaNacimientoValidador
      ]),
      contrasena: new FormControl(this.usuario.contrasena, contrasenaValidators),
      confirmarContrasena: new FormControl(this.usuario.confirmarContrasena, confirmarContrasenaValidators),
      activo: new FormControl(this.usuario.activo, [
        Validators.required
      ]),
      rol: new FormControl(idRoles, [ Validators.required ])
    });
    
    this.usuarioForm.setValidators(this._loginService.ConfirmarContraseñaValidador);

    if (!this._loginService.escritura)
      this.usuarioForm.disable();
  }
  //Metodos Formulario

  //Metodos Servicios
  Guardar() {
    this._usuarioService.GuardarUsuario(this.usuario).subscribe(
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

  ObtenerRoles() {
    this._rolService.ObtenerRoles('', '', false, 0, 0, '', false).subscribe(
      response => {
        this.roles = response.resultados;
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }
  //Metodos Servicios
}
