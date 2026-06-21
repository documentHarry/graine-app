import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AromateSupprimerComponent } from './aromate-supprimer.component';

describe('AromateSupprimerComponent', () => {
  let component: AromateSupprimerComponent;
  let fixture: ComponentFixture<AromateSupprimerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AromateSupprimerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AromateSupprimerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
