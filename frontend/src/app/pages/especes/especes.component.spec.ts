import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EspecesComponent } from './especes.component';

describe('EspecesComponent', () => {
  let component: EspecesComponent;
  let fixture: ComponentFixture<EspecesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EspecesComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EspecesComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
