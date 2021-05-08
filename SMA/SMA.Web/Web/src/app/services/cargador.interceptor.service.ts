import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable, pipe } from 'rxjs';
import { tap } from 'rxjs/operators';
import { CargadorService } from '../services/cargador.service';

@Injectable({
  providedIn: 'root'
})
export class CargadorInterceptorService implements HttpInterceptor {
  constructor(private _cargadorService: CargadorService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.mostrarCargador();
    return next.handle(req).pipe(tap((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse) {
        this.alTerminar();
      }
    },
      (err: any) => {
        this.alTerminar();
      }));
  }
  private alTerminar(): void {
    this.ocultarCargador();
  }
  private mostrarCargador(): void {
    this._cargadorService.mostrar();
  }
  private ocultarCargador(): void {
    this._cargadorService.ocultar();
  }
}
