import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ConstanteService {
  readonly PatternLetras = '^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ ]*$';
  constructor() { }
}
