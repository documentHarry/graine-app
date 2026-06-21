import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AromateModifierComponent } from './aromate-modifier.component';

describe('AromateModifierComponent', () => {
  let component: AromateModifierComponent;
  let fixture: ComponentFixture<AromateModifierComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AromateModifierComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AromateModifierComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
