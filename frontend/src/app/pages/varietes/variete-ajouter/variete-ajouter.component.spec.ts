import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VarieteAjouterComponent } from './variete-ajouter.component';

describe('VarieteAjouterComponent', () => {
  let component: VarieteAjouterComponent;
  let fixture: ComponentFixture<VarieteAjouterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VarieteAjouterComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(VarieteAjouterComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
