import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Observable } from 'rxjs';

import { ProprieteMedicinale } from '../../services/api/models/propriete-medicinale.model';
import { ProprieteMedicinaleService } from '../../services/api/propriete-medicinale.service';

@Component({
  selector: 'app-proprietes-medicinales',
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './proprietes-medicinales.component.html',
  styleUrls: ['./proprietes-medicinales.component.css']
})

export class ProprietesMedicinalesComponent {
  proprietes$: Observable<ProprieteMedicinale[]>;
  recherche = '';

  constructor(private proprieteMedicinaleService: ProprieteMedicinaleService) {
    this.proprietes$ = this.proprieteMedicinaleService.getProprietesMedicinales();
  }

  proprietesFiltrees(proprietes: ProprieteMedicinale[]): ProprieteMedicinale[] {
    return this.proprieteMedicinaleService.filtrerProprietesMedicinales(proprietes, this.recherche);
  }
}