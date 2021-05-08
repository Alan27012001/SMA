import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute  } from '@angular/router';

import { LoginService } from '../../services/login.service';
import { MensajeService } from '../../services/mensaje.service';
import { RestablecerContrasenaService } from '../../services/restablecerContrasena.service';

import { Usuario } from '../../models/usuario';

@Component({
  selector: 'app-restablecer-contrasena',
  templateUrl: './restablecer-contrasena.component.html',
  styleUrls: ['./restablecer-contrasena.component.css']
})
export class RestablecerContrasenaComponent implements OnInit {
  public id: string;
  public usuario: Usuario;
  public restablecerForm: FormGroup;

  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private _mensajeService: MensajeService,
    private _loginService: LoginService,
    private _restablecerContrasenaService: RestablecerContrasenaService
  ) { }

  ngOnInit(): void {
    this.usuario = new Usuario();
    this.CrearForm();
    this.id = this._route.snapshot.params.id;
    this._restablecerContrasenaService.ValidarId(this.id).subscribe(
      response => {
        this.usuario.id = response.id;
        console.log(response);
      },
      error => {
        this._router.navigate(['login']);
        ;
      }
    );
  }

  Aceptar() {
    this.usuario.contraseña = this.restablecerForm.value.contrasena;
    this.usuario.recuperar = this.id;
    this.Guardar();
  }

  Cancelar() {
    this._router.navigate(['login']);
  }

  //Metodos Formulario
  CrearForm() {
    this.restablecerForm = new FormGroup({
      contrasena: new FormControl('', [
        Validators.required,
        Validators.maxLength(50)
      ]),
      confirmarContrasena: new FormControl('', [
        Validators.required,
        Validators.maxLength(50)
      ])
    });
    this.restablecerForm.setValidators(this._loginService.ConfirmarContraseñaValidador);
  }
  //Metodos Formulario

  //Metodos Servicios
  Guardar() {
    this._restablecerContrasenaService.ActualizarContrasena(this.usuario).subscribe(
      response => {
        const guardar = this._mensajeService.Informacion(response);
        guardar.result.then((resultado) => {
          this._router.navigate(['login']);  
        });
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }
  //Metodos Servicios
}
