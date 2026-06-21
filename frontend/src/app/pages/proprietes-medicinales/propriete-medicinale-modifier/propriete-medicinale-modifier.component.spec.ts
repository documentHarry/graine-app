import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProprieteMedicinaleModifierComponent } from './propriete-medicinale-modifier.component';

describe('ProprieteMedicinaleModifierComponent', () => {
  let component: ProprieteMedicinaleModifierComponent;
  let fixture: ComponentFixture<ProprieteMedicinaleModifierComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProprieteMedicinaleModifierComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ProprieteMedicinaleModifierComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
