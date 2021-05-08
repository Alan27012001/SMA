import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReporteAdministradorComponent } from './reporte-administrador.component';

describe('ReporteAdministradorComponent', () => {
  let component: ReporteAdministradorComponent;
  let fixture: ComponentFixture<ReporteAdministradorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReporteAdministradorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReporteAdministradorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
