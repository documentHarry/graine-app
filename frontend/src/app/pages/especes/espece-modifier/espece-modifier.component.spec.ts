import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EspeceModifierComponent } from './espece-modifier.component';

describe('EspeceModifierComponent', () => {
  let component: EspeceModifierComponent;
  let fixture: ComponentFixture<EspeceModifierComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EspeceModifierComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EspeceModifierComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
