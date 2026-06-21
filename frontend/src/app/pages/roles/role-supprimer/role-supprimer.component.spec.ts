import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleSupprimerComponent } from './role-supprimer.component';

describe('RoleSupprimerComponent', () => {
  let component: RoleSupprimerComponent;
  let fixture: ComponentFixture<RoleSupprimerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoleSupprimerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RoleSupprimerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
