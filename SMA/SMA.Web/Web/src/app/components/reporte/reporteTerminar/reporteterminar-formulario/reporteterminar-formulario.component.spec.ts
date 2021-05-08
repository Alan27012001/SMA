import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReporteterminarFormularioComponent } from './reporteterminar-formulario.component';

describe('ReporteterminarFormularioComponent', () => {
  let component: ReporteterminarFormularioComponent;
  let fixture: ComponentFixture<ReporteterminarFormularioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReporteterminarFormularioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReporteterminarFormularioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
