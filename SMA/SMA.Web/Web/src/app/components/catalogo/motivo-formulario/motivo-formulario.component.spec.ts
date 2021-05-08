import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MotivoFormularioComponent } from './motivo-formulario.component';

describe('MotivoFormularioComponent', () => {
  let component: MotivoFormularioComponent;
  let fixture: ComponentFixture<MotivoFormularioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MotivoFormularioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MotivoFormularioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
