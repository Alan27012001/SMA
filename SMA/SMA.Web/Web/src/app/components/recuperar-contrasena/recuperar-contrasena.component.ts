import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { LoginService } from '../../services/login.service';
import { MensajeService } from '../../services/mensaje.service';

import { Usuario } from '../../models/usuario';

@Component({
  selector: 'app-recuperar-contrasena',
  templateUrl: './recuperar-contrasena.component.html',
  styleUrls: ['./recuperar-contrasena.component.css']
})
export class RecuperarContrasenaComponent implements OnInit {
  public recuperarForm: FormGroup;
  private usuario: Usuario;

  constructor(
    public activeModal: NgbActiveModal,
    private _loginService: LoginService,
    private _mensajeService: MensajeService
  ) {
    this.usuario = new Usuario();
  }

  ngOnInit(): void {
    this.CrearForm();
  }

  Aceptar() {
    this.usuario.correo = this.recuperarForm.value.correo;
    this.Enviar();
  }

  Cancelar() {
    this.activeModal.close('Close click');
  }

  //Metodos Formulario
  CrearForm() {
    this.recuperarForm = new FormGroup({
      correo: new FormControl('', [
        Validators.required,
        Validators.email,
        Validators.maxLength(80)
      ])
    });
  }
  //Metodos Formulario

  //Metodos Servicios
  Enviar() {
    console.log(this.usuario);
    this._loginService.RecuperarContrasena(this.usuario).subscribe(
      response => {
        this._mensajeService.Informacion(response);
        this.activeModal.close('enviado');
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }
  //Metodos Servicios
}
