import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Motivo } from '../../../models/motivo';

import { LoginService } from '../../../services/login.service';
import { MensajeService } from '../../../services/mensaje.service';
import { ConstanteService } from '../../../services/constante.service';

//Translate
import { TranslateService } from '@ngx-translate/core'

import { MotivoService } from '../../../services/motivo.service';

@Component({
  selector: 'app-motivo-formulario',
  templateUrl: './motivo-formulario.component.html',
  styleUrls: ['./motivo-formulario.component.css']
})
export class MotivoFormularioComponent implements OnInit {
  public motivo: Motivo;
  public motivoForm: FormGroup;

  constructor(
    public activeModal: NgbActiveModal,
    public _loginService: LoginService,
    private _mensajeService: MensajeService,
    private _constanteService: ConstanteService,
    private _motivoService: MotivoService,
    private translate: TranslateService) {
  }

  ngOnInit(): void {
    this._loginService.ValidarPantalla('motivo');
    this.CrearForm();
  }

  Aceptar() {
    this.motivo.motivo = this.motivoForm.value.motivo;
    this.motivo.descripcion = this.motivoForm.value.descripcion;
    this.motivo.activo = this.motivoForm.value.activo;
    this.Guardar();
  }

  Cancelar() {
    this.activeModal.close('Close click');
  }

  //Metodos Formulario
  CrearForm() {
    this.motivoForm = new FormGroup({
      motivo: new FormControl(this.motivo.motivo,
        [
          Validators.required,
          Validators.maxLength(80),
          Validators.pattern(this._constanteService.PatternLetras)
        ]),
      descripcion: new FormControl(this.motivo.descripcion, [
        Validators.required,
        Validators.maxLength(250)
      ]),
      activo: new FormControl(this.motivo.activo, [
        Validators.required
      ]),
    });

    if (!this._loginService.escritura)
      this.motivoForm.disable();
  }
  //Metodos Formulario

  //Metodos Servicios
  Guardar() {
    this._motivoService.GuardarMotivo(this.motivo).subscribe(
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
