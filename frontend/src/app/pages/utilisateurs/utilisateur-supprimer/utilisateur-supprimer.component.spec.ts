import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilisateurSupprimerComponent } from './utilisateur-supprimer.component';

describe('UtilisateurSupprimerComponent', () => {
  let component: UtilisateurSupprimerComponent;
  let fixture: ComponentFixture<UtilisateurSupprimerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UtilisateurSupprimerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UtilisateurSupprimerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
