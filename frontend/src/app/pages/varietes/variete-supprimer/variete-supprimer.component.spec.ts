import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VarieteSupprimerComponent } from './variete-supprimer.component';

describe('VarieteSupprimerComponent', () => {
  let component: VarieteSupprimerComponent;
  let fixture: ComponentFixture<VarieteSupprimerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VarieteSupprimerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(VarieteSupprimerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
