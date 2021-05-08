export class Paginacion {
  public actual: number;
  public cantidad: number;
  public total: number;
  public totalFilas: number;
  public primera: number;
  public ultima: number;
  public mostradas: number[];

  constructor() {
    this.actual = 1;
    this.primera = 1;
    this.ultima = 10;
    this.cantidad = 5;
    this.total = 0;
    this.totalFilas = 0;
    this.mostradas = [];
  }

  Actualizar(actual: number, cantidad: number, totalFilas: number, total: number) {
    this.actual = actual;
    this.cantidad = cantidad;
    this.totalFilas = totalFilas;
    this.total = total;
    this.Redistribuir();
  }

  Siguiente() {
    if (this.actual < this.total)
      this.actual++;
  }

  Anterior() {
    if (this.actual > 1)
      this.actual--;
  }

  Ir(pagina: number) {
    if (pagina <= this.total && pagina > 0)
      this.actual = pagina;
  }

  Redistribuir() {    
    if (this.actual > this.total || this.actual < 1)
      return;
    if ((this.ultima - this.primera) < 10 && this.total > this.ultima)
      this.ultima = this.primera + 9;

    this.mostradas = [];
    if (this.actual < this.primera) {
      if (this.actual > 10) {
        this.primera = this.actual - (this.actual % 10);
        this.primera = this.primera - 9;
      }
      else
        this.primera = 1;
      this.ultima = this.primera + 9;
    }
    else if (this.actual > this.ultima) {
      if (this.actual % 10 > 0) {
        this.ultima = this.actual - (this.actual % 10);
        this.ultima = this.ultima + 10;
      }
      else
        this.ultima = this.actual;
      this.primera = this.ultima - 9;
    }

    while (this.primera < 1)
      this.primera++;
    while (this.ultima > this.total)
      this.ultima--;
    for (let i = this.primera; i <= this.ultima; i++)
      this.mostradas.push(i);
  }
}
