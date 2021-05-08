import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MensajeInformacionComponent } from './mensaje-informacion.component';

describe('MensajeInformacionComponent', () => {
  let component: MensajeInformacionComponent;
  let fixture: ComponentFixture<MensajeInformacionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MensajeInformacionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MensajeInformacionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
