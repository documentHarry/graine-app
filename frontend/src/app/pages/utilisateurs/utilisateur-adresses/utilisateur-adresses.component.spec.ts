import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilisateurAdressesComponent } from './utilisateur-adresses.component';

describe('UtilisateurAdressesComponent', () => {
  let component: UtilisateurAdressesComponent;
  let fixture: ComponentFixture<UtilisateurAdressesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UtilisateurAdressesComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UtilisateurAdressesComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
