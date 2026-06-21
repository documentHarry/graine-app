import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EspeceSupprimerComponent } from './espece-supprimer.component';

describe('EspeceSupprimerComponent', () => {
  let component: EspeceSupprimerComponent;
  let fixture: ComponentFixture<EspeceSupprimerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EspeceSupprimerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EspeceSupprimerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
