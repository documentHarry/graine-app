import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VarietesComponent } from './varietes.component';

describe('VarietesComponent', () => {
  let component: VarietesComponent;
  let fixture: ComponentFixture<VarietesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VarietesComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(VarietesComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
