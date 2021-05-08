import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MensajeInformacionComponent } from '../components/mensajes/mensaje-informacion/mensaje-informacion.component';
import { MensajeErrorComponent } from '../components/mensajes/mensaje-error/mensaje-error.component';
import { MensajeConfirmacionComponent } from '../components/mensajes/mensaje-confirmacion/mensaje-confirmacion.component';

@Injectable({
  providedIn: 'root',
})
export class MensajeService {
  constructor(private modalService: NgbModal) {}

  Informacion (mensaje: string): any {
    const modalRef = this.modalService.open(MensajeInformacionComponent);
    modalRef.componentInstance.mensaje = mensaje;
    return modalRef;
  }

  Error (mensaje: string): any {
    const modalRef = this.modalService.open(MensajeErrorComponent);
    modalRef.componentInstance.mensaje = mensaje;
    return modalRef;
  }

  
  Confirmacion(mensaje: string): any {
    const modalRef = this.modalService.open(MensajeConfirmacionComponent);
    modalRef.componentInstance.mensaje = mensaje;
    return modalRef;
  }
}
