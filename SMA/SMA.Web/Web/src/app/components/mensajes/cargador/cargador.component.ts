import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { CargadorService } from '../../../services/cargador.service';
import { Cargador } from '../../../models/cargador';

@Component({
  selector: 'app-cargador',
  templateUrl: './cargador.component.html',
  styleUrls: ['./cargador.component.css']
})
export class CargadorComponent implements OnInit {
  mostrar = false;
  private subscription: Subscription;
  constructor(private _cargadorService: CargadorService) { }
  ngOnInit() {
    this.subscription = this._cargadorService.cargador
      .subscribe((estado: Cargador) => {
        this.mostrar = estado.mostrar;
      });
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
