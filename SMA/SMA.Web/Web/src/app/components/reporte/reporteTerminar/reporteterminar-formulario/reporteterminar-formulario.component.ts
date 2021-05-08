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
  selector: 'app-reporteterminar-formulario',
  templateUrl: './reporteterminar-formulario.component.html',
  styleUrls: ['./reporteterminar-formulario.component.css']
})
export class ReporteterminarFormularioComponent implements OnInit {
  public reporteCerrarReporteForm: FormGroup;
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
  }

  Aceptar() {
    this.reporte.fechaFinalizacion = this.reporteCerrarReporteForm.value.fechaFinalizacion;
    this.reporte.comentarioFinalizacion = this.reporteCerrarReporteForm.value.comentarioFinalizacion;
    console.log(this.reporte);
    this.Guardar();
  }

  Cancelar() {
    this.activeModal.close('Close click');
  }

  //Metodos Formulario
  CrearForm() {
    this.reporteCerrarReporteForm = new FormGroup({
      comentarioFinalizacion: new FormControl(this.reporte.comentarioFinalizacion, [
        Validators.maxLength(250)
      ]),
      fechaFinalizacion: new FormControl(this.fechas.transform(this.reporte.fechaFinalizacion, 'yyyy-MM-dd')),
      comentarioAsignacion: new FormControl(this.reporte.comentarioAsignacion, [
        Validators.maxLength(250)
      ]),
      fechaAsignacion: new FormControl(this.fechas.transform(this.reporte.fechaAsignacion, 'yyyy-MM-dd'))
    });
    if (!this._loginService.escritura)
      this.reporteCerrarReporteForm.disable();
  }
  //Metodos Formulario

  //Metodos Servicios
  Guardar() {
    this._reporteService.CerrarReporte(this.reporte).subscribe(
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
}
