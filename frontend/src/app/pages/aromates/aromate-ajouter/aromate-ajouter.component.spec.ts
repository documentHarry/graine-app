import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AromateAjouterComponent } from './aromate-ajouter.component';

describe('AromateAjouterComponent', () => {
  let component: AromateAjouterComponent;
  let fixture: ComponentFixture<AromateAjouterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AromateAjouterComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AromateAjouterComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
