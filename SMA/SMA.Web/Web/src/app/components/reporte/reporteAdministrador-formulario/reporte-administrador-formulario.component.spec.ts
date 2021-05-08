import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReporteAdministradorFormularioComponent } from './reporte-administrador-formulario.component';

describe('ReporteAdministradorFormularioComponent', () => {
  let component: ReporteAdministradorFormularioComponent;
  let fixture: ComponentFixture<ReporteAdministradorFormularioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReporteAdministradorFormularioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReporteAdministradorFormularioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
