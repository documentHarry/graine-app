import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilisateurAdresseModifierComponent } from './utilisateur-adresse-modifier.component';

describe('UtilisateurAdresseModifierComponent', () => {
  let component: UtilisateurAdresseModifierComponent;
  let fixture: ComponentFixture<UtilisateurAdresseModifierComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UtilisateurAdresseModifierComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UtilisateurAdresseModifierComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
