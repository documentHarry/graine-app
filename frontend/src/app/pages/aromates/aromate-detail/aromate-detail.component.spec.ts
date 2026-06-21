import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AromateDetailComponent } from './aromate-detail.component';

describe('AromateDetailComponent', () => {
  let component: AromateDetailComponent;
  let fixture: ComponentFixture<AromateDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AromateDetailComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AromateDetailComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
