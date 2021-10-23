import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';

import { Reporte } from '../../../models/reporte';
import { Motivo } from '../../../models/motivo';
import { Proyecto } from '../../../models/proyecto';
import { Estatus } from '../../../models/estatus';
import { Usuario } from '../../../models/usuario';

import { LoginService } from '../../../services/login.service';
import { MensajeService } from '../../../services/mensaje.service';
import { ConstanteService } from '../../../services/constante.service';
import { ReporteService } from '../../../services/reporte.service';
import { MotivoService } from '../../../services/motivo.service';
import { ProyectoService } from '../../../services/proyecto.service';
import { EstatusService } from '../../../services/estatus.service';
import { UsuarioService } from '../../../services/usuario.service';

//Translate
import { TranslateService } from '@ngx-translate/core'

@Component({
  selector: 'app-reporte-administrador-formulario',
  templateUrl: './reporte-administrador-formulario.component.html',
  styleUrls: ['./reporte-administrador-formulario.component.css']
})
export class ReporteAdministradorFormularioComponent implements OnInit {
  public reporteForm: FormGroup;
  public reporte: Reporte;
  public proyectos: Proyecto[];
  public motivos: Motivo[];
  public estatus: Estatus[];
  public usuarios: Usuario[];

  constructor(
    public activeModal: NgbActiveModal,
    public fechas: DatePipe,
    public _loginService: LoginService,
    private _mensajeService: MensajeService,
    private _constanteService: ConstanteService,
    private _reporteService: ReporteService,
    private _motivoService: MotivoService,
    private _proyectoService: ProyectoService,
    private _estatusService: EstatusService,
    private _usuarioService: UsuarioService,
    private translate: TranslateService) {
  }

  ngOnInit(): void {
    this._loginService.ValidarPantalla('reporteadministrador');
    this.CrearForm();
    this.ObtenerMotivos();
    this.ObtenerProyectos();
    this.ObtenerUsuarios();
  }

  Aceptar() {
    var motivo = new Motivo();
    motivo.id = this.reporteForm.value.motivo;
    if (this.reporteForm.value.motivo !== null && this.reporteForm.value.motivo !== undefined)
      motivo.id = this.reporteForm.value.motivo;

    var proyecto = new Proyecto();
    proyecto.id = this.reporteForm.value.proyecto;

    if (this.reporteForm.value.proyecto !== null && this.reporteForm.value.proyecto !== undefined)
      proyecto.id = this.reporteForm.value.proyecto;

    this.reporte.motivo = motivo;
    this.reporte.idMotivo = motivo.id;
    this.reporte.proyecto = proyecto;
    this.reporte.idProyecto = proyecto.id;
    this.reporte.fechaReporte = this.reporteForm.value.fechaReporte;
    this.reporte.comentarioReporte = this.reporteForm.value.comentarioReporte;
    this.Guardar();
  }

  Cancelar() {
    this.activeModal.close('Close click');
  }

  //Metodos Formulario
  CrearForm() {
    this.reporteForm = new FormGroup({
      motivo: new FormControl(this.reporte.motivo === null || this.reporte.motivo === undefined ? '' : this.reporte.motivo.id, [Validators.required]),
      proyecto: new FormControl(this.reporte.proyecto === null || this.reporte.proyecto === undefined ? '' : this.reporte.proyecto.id, [Validators.required]),
      comentarioReporte: new FormControl(this.reporte.comentarioReporte, [
        Validators.maxLength(250)
      ]),
      fechaReporte: new FormControl(this.fechas.transform(this.reporte.fechaReporte, 'yyyy-MM-dd'))
    });
    if (!this._loginService.escritura)
      this.reporteForm.disable();
  }
  //Metodos Formulario

  //Metodos Servicios
  Guardar() {
    this._reporteService.GuardarReporteAdminstrador(this.reporte).subscribe(
      response => {
        this.ObtenerFolio(response);
        this._mensajeService.Informacion(response);
        this.activeModal.close('guardado');
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }

  ObtenerFolio(folio: string) {
    this._reporteService.ObtenerFolio(folio).subscribe(
      response => {
        var formato = 'Tu número de folio es: RP00';
        this._mensajeService.Informacion(formato + response);
        this.activeModal.close('guardado');
      },
      error => {
        if (!this._loginService.ManejarError(error))
          return;
      }
    );
  }
  //Metodos Servicios

  //Metodos de Combos
  ObtenerMotivos() {
    this._motivoService.ObtenerMotivos('', '', false, 0, 0, '', false).subscribe(
      response => {
        this.motivos = response.resultados;
      },
      error => {
        if (this._loginService.ManejarError(error))
          return;
      }
    );
  }

  ObtenerProyectos() {
    this._proyectoService.ObtenerProyectos('', '', false, 0, 0, '', false).subscribe(
      response => {
        this.proyectos = response.resultados;
      },
      error => {
        if (this._loginService.ManejarError(error))
          return;
      }
    );
  }

  ObtenerUsuarios() {
    this._usuarioService.ObtenerUsuariosAdministradores('', false, 0, 0, '', false).subscribe(
      response => {
        this.usuarios = response.resultados;
      },
      error => {
        if (this._loginService.ManejarError(error))
          return;
      }
    );
  }
  //Metodos de Combos
}
