import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleAjouterComponent } from './role-ajouter.component';

describe('RoleAjouterComponent', () => {
  let component: RoleAjouterComponent;
  let fixture: ComponentFixture<RoleAjouterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoleAjouterComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RoleAjouterComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
