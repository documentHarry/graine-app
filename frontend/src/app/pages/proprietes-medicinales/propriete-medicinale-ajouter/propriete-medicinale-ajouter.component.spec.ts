import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProprieteMedicinaleAjouterComponent } from './propriete-medicinale-ajouter.component';

describe('ProprieteMedicinaleAjouterComponent', () => {
  let component: ProprieteMedicinaleAjouterComponent;
  let fixture: ComponentFixture<ProprieteMedicinaleAjouterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProprieteMedicinaleAjouterComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ProprieteMedicinaleAjouterComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
