import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilisateurDetailComponent } from './utilisateur-detail.component';

describe('UtilisateurDetailComponent', () => {
  let component: UtilisateurDetailComponent;
  let fixture: ComponentFixture<UtilisateurDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UtilisateurDetailComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UtilisateurDetailComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
