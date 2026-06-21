import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VarieteDetailComponent } from './variete-detail.component';

describe('VarieteDetailComponent', () => {
  let component: VarieteDetailComponent;
  let fixture: ComponentFixture<VarieteDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VarieteDetailComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(VarieteDetailComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
