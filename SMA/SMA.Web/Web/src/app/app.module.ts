import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { LOCALE_ID, NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import { DatePipe } from '@angular/common';
import localeEs from '@angular/common/locales/es';

//Translation
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

registerLocaleData(localeEs, 'es');

import { CargadorInterceptorService } from './services/cargador.interceptor.service';
import { MensajeService } from './services/mensaje.service';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { MensajeInformacionComponent } from './components/mensajes/mensaje-informacion/mensaje-informacion.component';
import { MensajeErrorComponent } from './components/mensajes/mensaje-error/mensaje-error.component';
import { MensajeConfirmacionComponent } from './components/mensajes/mensaje-confirmacion/mensaje-confirmacion.component';
import { HomeComponent } from './components/home/home.component';
import { UsuarioComponent } from './components/seguridad/usuario/usuario.component';
import { MenuComponent } from './components/menu/menu.component';
import { UsuarioFormularioComponent } from './components/seguridad/usuario-formulario/usuario-formulario.component';
import { CargadorComponent } from './components/mensajes/cargador/cargador.component';
import { RecuperarContrasenaComponent } from './components/recuperar-contrasena/recuperar-contrasena.component';
import { RestablecerContrasenaComponent } from './components/restablecer-contrasena/restablecer-contrasena.component';
import { RolComponent } from './components/seguridad/rol/rol.component';
import { RolFormularioComponent } from './components/seguridad/rol-formulario/rol-formulario.component';
import { ProyectoComponent } from './components/catalogo/proyecto/proyecto.component';
import { ProyectoFormularioComponent } from './components/catalogo/proyecto-formulario/proyecto-formulario.component';
import { MotivoComponent } from './components/catalogo/motivo/motivo.component';
import { MotivoFormularioComponent } from './components/catalogo/motivo-formulario/motivo-formulario.component';
import { ReporteComponent } from './components/reporte/reporte/reporte.component';
import { ReporteAdministradorComponent } from './components/reporte/reporteAdministrador/reporte-administrador.component';
import { ReporteFormularioComponent } from './components/reporte/reporte-formulario/reporte-formulario.component';
import { ReporteAdministradorFormularioComponent } from './components/reporte/reporteAdministrador-formulario/reporte-administrador-formulario.component';
import { ReporteasignarFormularioComponent } from './components/reporte/reporteAsignar/reporteasignar-formulario/reporteasignar-formulario.component';
import { ReporteterminarFormularioComponent } from './components/reporte/reporteTerminar/reporteterminar-formulario/reporteterminar-formulario.component';
import { ReporteusuarioterminarFormularioComponent } from './components/reporte/reporteUsuarioTerminar/reporteusuarioterminar-formulario/reporteusuarioterminar-formulario.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MensajeInformacionComponent,
    MensajeErrorComponent,
    MensajeConfirmacionComponent,
    HomeComponent,
    UsuarioComponent,
    MenuComponent,
    UsuarioFormularioComponent,
    CargadorComponent,
    RecuperarContrasenaComponent,
    RestablecerContrasenaComponent,
    RolComponent,
    RolFormularioComponent,
    ProyectoComponent,
    ProyectoFormularioComponent,
    MotivoComponent,
    MotivoFormularioComponent,
    ReporteComponent,
    ReporteAdministradorComponent,
    ReporteFormularioComponent,
    ReporteAdministradorFormularioComponent,
    ReporteasignarFormularioComponent,
    ReporteterminarFormularioComponent,
    ReporteusuarioterminarFormularioComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (http: HttpClient) => {
          return new TranslateHttpLoader(http);
        },
        deps: [HttpClient]
      }
    }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'home', component: HomeComponent },
      { path: 'usuario', component: UsuarioComponent },
      { path: 'rol', component: RolComponent },
      { path: 'restablecerContrasena/:id', component: RestablecerContrasenaComponent },
      { path: 'proyecto', component: ProyectoComponent },
      { path: 'motivo', component: MotivoComponent },
      { path: 'reporte', component: ReporteComponent },
      { path: 'reporteadministrador', component: ReporteAdministradorComponent }
    ])],
  providers: [
    DatePipe,
    { provide: LOCALE_ID, useValue: 'es' },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CargadorInterceptorService,
      multi: true
    },
    MensajeService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
