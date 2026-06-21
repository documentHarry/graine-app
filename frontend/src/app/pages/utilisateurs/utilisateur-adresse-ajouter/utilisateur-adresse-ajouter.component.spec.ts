import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilisateurAdresseAjouterComponent } from './utilisateur-adresse-ajouter.component';

describe('UtilisateurAdresseAjouterComponent', () => {
  let component: UtilisateurAdresseAjouterComponent;
  let fixture: ComponentFixture<UtilisateurAdresseAjouterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UtilisateurAdresseAjouterComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UtilisateurAdresseAjouterComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
