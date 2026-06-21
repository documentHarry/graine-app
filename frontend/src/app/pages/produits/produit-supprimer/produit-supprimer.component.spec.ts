import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProduitSupprimerComponent } from './produit-supprimer.component';

describe('ProduitSupprimerComponent', () => {
  let component: ProduitSupprimerComponent;
  let fixture: ComponentFixture<ProduitSupprimerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProduitSupprimerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ProduitSupprimerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
