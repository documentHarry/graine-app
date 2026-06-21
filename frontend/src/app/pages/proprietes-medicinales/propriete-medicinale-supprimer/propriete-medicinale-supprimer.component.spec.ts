import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProprieteMedicinaleSupprimerComponent } from './propriete-medicinale-supprimer.component';

describe('ProprieteMedicinaleSupprimerComponent', () => {
  let component: ProprieteMedicinaleSupprimerComponent;
  let fixture: ComponentFixture<ProprieteMedicinaleSupprimerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProprieteMedicinaleSupprimerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ProprieteMedicinaleSupprimerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
