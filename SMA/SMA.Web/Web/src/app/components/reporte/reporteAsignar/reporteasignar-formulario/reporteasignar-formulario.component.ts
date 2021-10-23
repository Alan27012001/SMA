import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';

import { Reporte } from '../../../../models/reporte';
import { Motivo } from '../../../../models/motivo';
import { Proyecto } from '../../../../models/proyecto';
import { Estatus } from '../../../../models/estatus';
import { Usuario } from '../../../../models/usuario';

import { LoginService } from '../../../../services/login.service';
import { MensajeService } from '../../../../services/mensaje.service';
import { ConstanteService } from '../../../../services/constante.service';
import { ReporteService } from '../../../../services/reporte.service';
import { MotivoService } from '../../../../services/motivo.service';
import { ProyectoService } from '../../../../services/proyecto.service';
import { EstatusService } from '../../../../services/estatus.service';
import { UsuarioService } from '../../../../services/usuario.service';

//Translate
import { TranslateService } from '@ngx-translate/core'

@Component({
  selector: 'app-reporteasignar-formulario',
  templateUrl: './reporteasignar-formulario.component.html',
  styleUrls: ['./reporteasignar-formulario.component.css']
})
export class ReporteasignarFormularioComponent implements OnInit {
  public reporteAsginarForm: FormGroup;
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
    this.ObtenerUsuarios();
  }

  Aceptar() {
    var usuario = new Usuario();
    usuario.id = this.reporteAsginarForm.value.usuario;
    if (this.reporteAsginarForm.value.usuario !== null && this.reporteAsginarForm.value.usuario !== undefined)
      usuario.id = this.reporteAsginarForm.value.usuario;

    this.reporte.usuario = usuario;
    this.reporte.idUsuario = usuario.id;
    this.reporte.fechaAsignacion = this.reporteAsginarForm.value.fechaAsignacion;
    this.reporte.comentarioAsignacion = this.reporteAsginarForm.value.comentarioAsignacion;
    console.log(this.reporte);
    this.Guardar();
  }

  Cancelar() {
    this.activeModal.close('Close click');
  }

  //Metodos Formulario
  CrearForm() {
    this.reporteAsginarForm = new FormGroup({
      usuario: new FormControl(this.reporte.usuario === null || this.reporte.usuario === undefined ? '' : this.reporte.usuario.id, [Validators.required]),
      comentarioAsignacion: new FormControl(this.reporte.comentarioAsignacion, [
        Validators.maxLength(250)
      ]),
      fechaAsignacion: new FormControl(this.fechas.transform(this.reporte.fechaAsignacion, 'yyyy-MM-dd')),
      comentarioReporte: new FormControl(this.reporte.comentarioReporte, [
        Validators.maxLength(250)
      ]),
      fechaReporte: new FormControl(this.fechas.transform(this.reporte.fechaReporte, 'yyyy-MM-dd'))
    });
    if (!this._loginService.escritura)
      this.reporteAsginarForm.disable();
  }
  //Metodos Formulario

  //Metodos Servicios
  Guardar() {
    this._reporteService.AsignarReporte(this.reporte).subscribe(
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
  //Metodos Servicios

  //Metodos de Combos
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
