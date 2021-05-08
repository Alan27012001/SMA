import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormControl, AbstractControl } from '@angular/forms';
import { MensajeService } from '../services/mensaje.service';
import { Usuario } from '../models/usuario';
import { Permiso } from '../models/permiso';
import { Menu } from '../models/menu';
import { Parametro } from '../models/parametro';
import { Global } from './global';
import { TranslateService } from '@ngx-translate/core';

@Injectable({providedIn: 'root'})
export class LoginService {
  public url: string;
  public lectura: boolean;
  public escritura: boolean;
  public activeLang = 'es-MX';

  private permisos: Array<Permiso> = [];

  constructor(
    private _router: Router,
    private _mensajeService: MensajeService,
    private _http: HttpClient,
    private translate: TranslateService
  ) {
    this.url = Global.url;
  }

  /*Sesion*/
  public EstaLogin(): boolean {
    return (localStorage.getItem('auth_token') !== null && localStorage.getItem('auth_token') !== undefined);
  }

  public Login(usuario: Usuario): void {
    this.ServicioPostSinToken('Seguridad/Login', usuario).subscribe(
      response => {
        localStorage.setItem('auth_token', response.token);
        localStorage.setItem('usuario', JSON.stringify(response.usuario));
        localStorage.setItem('menu', JSON.stringify(response.menu));
        this._router.navigate(['home']);
      },
      error => {
        this.ManejarError(error);
      }
    );
  }

  public Logout() {
    if (!this.EstaLogin()) {
      localStorage.removeItem('auth_token');
      localStorage.removeItem('usuario');
      localStorage.removeItem('menu');
      this._router.navigate(['login']);
    }
    else {
      this.ServicioPost('Seguridad/Logout', '').subscribe(
          response => {
            localStorage.removeItem('auth_token');
            localStorage.removeItem('usuario');
            localStorage.removeItem('menu');
            this._router.navigate(['login']);
          },
          error => {
            localStorage.removeItem('auth_token');
            localStorage.removeItem('usuario');
            localStorage.removeItem('menu');
            this._router.navigate(['login']);
          }
        );
    }
  }

  public RecuperarContrasena(correo: Usuario): any {
    return this.ServicioPostSinToken('Seguridad/RecuperarContrasena', correo, true);
  }

  public ValidarPantalla(pantalla: string) {
    const pantallaParametro = new Parametro('llavePantalla', pantalla);
    const parametros = [pantallaParametro];
    this.permisos = [];
    this.ServicioGet('Seguridad/ObtenerPermisos', parametros).subscribe(
      response => {
        if (response.permisos === null || response.permisos === undefined)
          this._router.navigate(['home']);
        else if (response.permisos.length === 0)
          this._router.navigate(['home']);
        if (response.menu !== null && response.menu !== undefined)
          localStorage.setItem('menu', JSON.stringify(response.menu));

        for (let i = 0; i < response.permisos.length; i++) {
          const permiso = new Permiso();
          permiso.idPermiso = response.permisos[i].id;
          permiso.permiso = response.permisos[i].nombre;
          permiso.llave = response.permisos[i].llave;
          this.permisos.push(permiso);
        }

        this.TieneLectura();
        this.TieneEscritura();
      },
      error => {
        if (!this.ManejarError(error))
          return;
      }
    );
  }

  private TieneLectura(): void {
    const lectura = this.permisos.filter(p => p.llave.toLowerCase() === 'read');
    if (lectura === null || lectura === undefined)
      this.lectura = false;
    this.lectura = lectura.length > 0;
  }

  private TieneEscritura(): void {
    const escritura = this.permisos.filter(p => p.llave.toLowerCase() === 'write');
    if (escritura === null || escritura === undefined)
      this.escritura = false;
    this.escritura = escritura.length > 0;
  }

  public ObtenerMenu(): Menu[] {
    return JSON.parse(localStorage.getItem('menu'));
  }

  public ObtenerUsuario(): Usuario {
    return JSON.parse(localStorage.getItem('usuario'));
  }
  /*Sesion*/

  /*Llamada a servicios*/
  public ServicioGet(accion: string, parametros: Parametro[], esTexto = false): any {
    if (!this.EstaLogin()) {
      this._router.navigate(['login']);
      return;
    }

    const encabezado = new HttpHeaders(
      { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('auth_token') }
    );
    const responseType = esTexto ? 'text' : 'json';
    const tituloEncabezado = {
      headers: encabezado,
      responseType: responseType as 'json'
    };

    let valores = '';
    if (parametros !== null) {
      for (let i = 0; i < parametros.length; i++) {
        if (i === 0)
          valores += '?' + parametros[i].nombre + '=' + parametros[i].valor;
        else
          valores += '&' + parametros[i].nombre + '=' + parametros[i].valor;
      }
    }

    return this._http.get<any>(this.url + accion + valores, tituloEncabezado);
  }

  public ServicioPost(accion: string, parametro: any, esTexto = false): any {
    if (!this.EstaLogin()) {
      if (accion !== 'Seguridad/Login') {
        this._router.navigate(['login']);
        return;
      }
    }
    const encabezado = new HttpHeaders(
      {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('auth_token')
      }
    );
    const responseType = esTexto ? 'text' : 'json';
    const tituloEncabezado = {
      headers: encabezado,
      responseType: responseType as 'json'
    };

    return this._http.post<any>(this.url + accion, parametro, tituloEncabezado);
  }

  public ServicioGetSinToken(accion: string, parametros: Parametro[], esTexto = false): any {
    const encabezado = new HttpHeaders(
      { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('auth_token') }
    );
    const responseType = esTexto ? 'text' : 'json';
    const tituloEncabezado = {
      headers: encabezado,
      responseType: responseType as 'json'
    };

    let valores = '';
    if (parametros !== null) {
      for (let i = 0; i < parametros.length; i++) {
        if (i === 0)
          valores += '?' + parametros[i].nombre + '=' + parametros[i].valor;
        else
          valores += '&' + parametros[i].nombre + '=' + parametros[i].valor;
      }
    }

    return this._http.get<any>(this.url + accion + valores, tituloEncabezado);
  }

  public ServicioPostSinToken(accion: string, parametro: any, esTexto = false): any {
    const encabezado = new HttpHeaders(
      {
        'Content-Type': 'application/json'
      }
    );
    const responseType = esTexto ? 'text' : 'json';
    const tituloEncabezado = {
      headers: encabezado,
      responseType: responseType as 'json'
    };
    return this._http.post<any>(this.url + accion, parametro, tituloEncabezado);
  }

  public ManejarError(error): boolean {
    if (error === null) {
      this._mensajeService.Error('Error inesperado. Intente más tarde');
      return false;
    }
    if (error.status === 200)
      return true;
    else if (error.status === 0) {
      this._mensajeService.Error(error.message);
      return false;
    }
    else if (error.status === 401) {
      this.Logout();
      return false;
    }
    else {
      if (error.error !== null && error.error !== undefined) {
        if (error.error.title !== null && error.error.title !== undefined)
          this._mensajeService.Error(error.error.title);
        else
          this._mensajeService.Error(error.error);
      }
      else
        this._mensajeService.Error(error.message);
      return false;
    }
  }
  /*Llamada a servicios*/

  /*Validaciones*/
  public FechaNacimientoValidador(control: FormControl) {
    if (control === null)
      return null;
    if (control.value === null)
      return null;

    const fecha: Date = new Date(control.value.replace('-', ','));
    const hoy: Date = new Date();
    fecha.setHours(0, 0, 0, 0);
    hoy.setHours(0, 0, 0, 0);
    if (fecha > hoy)
      return {
        fechaMayor: {
          error: 'Fecha es mayor a hoy'
        }
      };
    return null;
  }

  public ConfirmarContraseñaValidador(control: AbstractControl) {
    let contrasena = '';
    let confirmarContrasena = '';
    const controlContrasena = control.get('contrasena');
    const controlConfirmarContrasena = control.get('confirmarContrasena');

    if (controlContrasena === null || controlContrasena === undefined)
      contrasena = '';
    else if (controlContrasena.value === null || controlContrasena.value === undefined)
      contrasena = '';
    else
      contrasena = controlContrasena.value;

    if (controlConfirmarContrasena === null || controlConfirmarContrasena === undefined)
      confirmarContrasena = '';
    else if (controlConfirmarContrasena.value === null || controlConfirmarContrasena.value === undefined)
      confirmarContrasena = '';
    else
      confirmarContrasena = controlConfirmarContrasena.value;

    if (contrasena !== confirmarContrasena) {
      if (controlConfirmarContrasena !== null && controlConfirmarContrasena !== undefined) {
        controlConfirmarContrasena.setErrors({ contrasenasNoCoinciden: true });
        if (controlContrasena.dirty)
          controlConfirmarContrasena.markAsDirty();
      }
    }
    else {
      if (controlConfirmarContrasena !== null && controlConfirmarContrasena !== undefined) {
        const errores = controlConfirmarContrasena.errors;
        if (errores) {
          delete errores['contrasenasNoCoinciden'];
          if (errores.length > 0)
            controlConfirmarContrasena.setErrors(errores);
          else
            controlConfirmarContrasena.setErrors(controlConfirmarContrasena.validator(controlConfirmarContrasena));
          if (controlContrasena.dirty)
            controlConfirmarContrasena.markAsDirty();
        }
      }
    }
    return null;
  }
/*validaciones*/
/*Idiomas del Login*/
  cambiarIdioma(lang) {
    this.activeLang = lang;
    localStorage.setItem('lang', lang);
    this.translate.use(lang);
  }

  iniciarIdioma() {
    if (localStorage.getItem('lang') === null || localStorage.getItem('lang') === undefined)
      this.activeLang = 'es-MX';
    else if (localStorage.getItem('lang') !== 'es-MX' && localStorage.getItem('lang') !== 'en-US')
      this.activeLang = 'es-MX';
    else
      this.activeLang = localStorage.getItem('lang');
    this.translate.setDefaultLang(this.activeLang);
    this.cambiarIdioma(this.activeLang);
  }

  obtenerIdiomaURL() {
    this.iniciarIdioma();
    let url = this.url.replace('{Culture}', this.activeLang);
    return url;
  }

  public obtenerIdioma() {
    this.iniciarIdioma();
    if (this.activeLang === 'en-US')
      return 'en';
    return 'es';
  }
}
