import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilisateurRolesComponent } from './utilisateur-roles.component';

describe('UtilisateurRolesComponent', () => {
  let component: UtilisateurRolesComponent;
  let fixture: ComponentFixture<UtilisateurRolesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UtilisateurRolesComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UtilisateurRolesComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
