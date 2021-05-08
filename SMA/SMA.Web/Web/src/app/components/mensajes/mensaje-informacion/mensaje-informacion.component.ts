import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-mensaje-informacion',
  templateUrl: './mensaje-informacion.component.html',
  styleUrls: ['./mensaje-informacion.component.css']
})
export class MensajeInformacionComponent {
  public mensaje: string;

  constructor(public activeModal: NgbActiveModal) { }
}
