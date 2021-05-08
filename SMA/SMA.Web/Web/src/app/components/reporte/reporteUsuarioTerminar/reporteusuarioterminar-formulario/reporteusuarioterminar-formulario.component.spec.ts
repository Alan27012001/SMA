import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReporteusuarioterminarFormularioComponent } from './reporteusuarioterminar-formulario.component';

describe('ReporteusuarioterminarFormularioComponent', () => {
  let component: ReporteusuarioterminarFormularioComponent;
  let fixture: ComponentFixture<ReporteusuarioterminarFormularioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReporteusuarioterminarFormularioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReporteusuarioterminarFormularioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
