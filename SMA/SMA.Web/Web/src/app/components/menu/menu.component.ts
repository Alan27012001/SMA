import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { Menu } from '../../models/menu';
import { Usuario } from '../../models/usuario';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  public collapsed = true;
  public menu: Menu[];
  public activeLang = 'es';
  public usuario: Usuario;

  constructor(
    private _router: Router,
    private _loginService: LoginService,
    private translate: TranslateService ) { }

  ngOnInit(): void {
    this.menu = this._loginService.ObtenerMenu();
    this.usuario = this._loginService.ObtenerUsuario();
    this.translate.setDefaultLang(this.activeLang);
  }

  logout() {
    this._loginService.Logout();
  }

  cambiarLenguaje(lang) {
    this.activeLang = lang;
    this.translate.use(lang);
  }
}
