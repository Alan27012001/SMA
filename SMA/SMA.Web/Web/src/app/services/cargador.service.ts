import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Cargador } from '../models/cargador';

@Injectable({
  providedIn: 'root'
})
export class CargadorService {
  private cargadorSubject = new Subject<Cargador>();
  cargador = this.cargadorSubject.asObservable();
  constructor() { }
  mostrar() {
    this.cargadorSubject.next(<Cargador>{ mostrar: true });
  }
  ocultar() {
    this.cargadorSubject.next(<Cargador>{ mostrar: false });
  }
}
