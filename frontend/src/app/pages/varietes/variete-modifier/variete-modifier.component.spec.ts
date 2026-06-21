import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VarieteModifierComponent } from './variete-modifier.component';

describe('VarieteModifierComponent', () => {
  let component: VarieteModifierComponent;
  let fixture: ComponentFixture<VarieteModifierComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VarieteModifierComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(VarieteModifierComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
