import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReporteasignarFormularioComponent } from './reporteasignar-formulario.component';

describe('ReporteasignarFormularioComponent', () => {
  let component: ReporteasignarFormularioComponent;
  let fixture: ComponentFixture<ReporteasignarFormularioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReporteasignarFormularioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReporteasignarFormularioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
