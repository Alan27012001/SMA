import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Usuario } from '../../models/usuario';
import { RecuperarContrasenaComponent } from '../recuperar-contrasena/recuperar-contrasena.component';
import { LoginService } from '../../services/login.service';
import { MensajeService } from '../../services/mensaje.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [LoginService]
})

export class LoginComponent implements OnInit {
  public usuario: Usuario;
  private recuperarConfig: NgbModalOptions = {};

  constructor(
    private _router: Router,
    private _modalService: NgbModal,
    private _mensajeService: MensajeService,
    public _loginService: LoginService,
    private translate: TranslateService
  ) {
    this.usuario = new Usuario();

    this.recuperarConfig.backdrop = 'static';
    this.recuperarConfig.keyboard = true;
    this.recuperarConfig.centered = true;
    this.recuperarConfig.scrollable = false;
    this.recuperarConfig.size = 'lg';
  }

  ngOnInit(): void {
    this._loginService.iniciarIdioma();
  }

  login() {
    if (this.usuario === null || this.usuario === undefined) {
      this.translate.get('mensajes.usuarioContraseñaRequerido').subscribe((msg: string) => {
        this._mensajeService.Error(msg);
      });
      return;
    }
    if (this.usuario.correo === null ||
      this.usuario.correo === undefined ||
      this.usuario.correo === '' ||
      this.usuario.contrasena === null ||
      this.usuario.contrasena === undefined ||
      this.usuario.contrasena === '') {
      this.translate.get('mensajes.usuarioContraseñaRequerido').subscribe((msg: string) => {
        this._mensajeService.Error(msg);
      });
    return;
    }
    this.usuario.contraseña = this.usuario.contrasena;
    this._loginService.Login(this.usuario);
  }

  recuperarContrasena() {
    this._modalService.open(RecuperarContrasenaComponent, this.recuperarConfig);
  }
}
