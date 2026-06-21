import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategorieSupprimerComponent } from './categorie-supprimer.component';

describe('CategorieSupprimerComponent', () => {
  let component: CategorieSupprimerComponent;
  let fixture: ComponentFixture<CategorieSupprimerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CategorieSupprimerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CategorieSupprimerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
