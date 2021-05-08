import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-mensaje-confirmacion',
  templateUrl: './mensaje-confirmacion.component.html',
  styleUrls: ['./mensaje-confirmacion.component.css']
})
export class MensajeConfirmacionComponent {
  public mensaje: string;

  constructor(public activeModal: NgbActiveModal) { }

  aceptar() {
    this.activeModal.close(true);
  }

  cancelar() {
    this.activeModal.close(false);
  }
}
