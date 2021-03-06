import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProyectoFormularioComponent } from './proyecto-formulario.component';

describe('ProyectoFormularioComponent', () => {
  let component: ProyectoFormularioComponent;
  let fixture: ComponentFixture<ProyectoFormularioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProyectoFormularioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProyectoFormularioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
