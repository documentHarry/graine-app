import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EspeceAjouterComponent } from './espece-ajouter.component';

describe('EspeceAjouterComponent', () => {
  let component: EspeceAjouterComponent;
  let fixture: ComponentFixture<EspeceAjouterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EspeceAjouterComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EspeceAjouterComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
