import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilisateurAdresseSupprimerComponent } from './utilisateur-adresse-supprimer.component';

describe('UtilisateurAdresseSupprimerComponent', () => {
  let component: UtilisateurAdresseSupprimerComponent;
  let fixture: ComponentFixture<UtilisateurAdresseSupprimerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UtilisateurAdresseSupprimerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UtilisateurAdresseSupprimerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
