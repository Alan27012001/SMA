import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Proyecto } from '../../../models/proyecto';

import { LoginService } from '../../../services/login.service';
import { MensajeService } from '../../../services/mensaje.service';
import { ConstanteService } from '../../../services/constante.service';
import { ProyectoService } from '../../../services/proyecto.service';

//Translate
import { TranslateService } from '@ngx-translate/core'


@Component({
  selector: 'app-proyecto-formulario',
  templateUrl: './proyecto-formulario.component.html',
  styleUrls: ['./proyecto-formulario.component.css']
})
export class ProyectoFormularioComponent implements OnInit {
  public proyecto: Proyecto;
  public proyectoForm: FormGroup;

  constructor(
    public activeModal: NgbActiveModal,
    public _loginService: LoginService,
    private _mensajeService: MensajeService,
    private _constanteService: ConstanteService,
    private _proyectoService: ProyectoService,
    private translate: TranslateService) {
  }

  ngOnInit(): void {
    this._loginService.ValidarPantalla('proyecto');
    this.CrearForm();
  }

  Aceptar() {
    this.proyecto.nombre = this.proyectoForm.value.nombre;
    this.proyecto.descripcion = this.proyectoForm.value.descripcion;
     this.proyecto.activo = this.proyectoForm.value.activo;
    this.Guardar();
  }

  Cancelar() {
    this.activeModal.close('Close click');
  }

  //Metodos Formulario
  CrearForm() {
    this.proyectoForm = new FormGroup({
      nombre: new FormControl(this.proyecto.nombre,
        [
          Validators.required,
          Validators.maxLength(80),
          Validators.pattern(this._constanteService.PatternLetras)
        ]),
      descripcion: new FormControl(this.proyecto.descripcion, [
        Validators.required,
        Validators.maxLength(250)
      ]),
      activo: new FormControl(this.proyecto.activo, [
        Validators.required
      ]),
    });

    if (!this._loginService.escritura)
      this.proyectoForm.disable();
  }
  //Metodos Formulario

  //Metodos Servicios
  Guardar() {
    this._proyectoService.GuardarProyecto(this.proyecto).subscribe(
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
