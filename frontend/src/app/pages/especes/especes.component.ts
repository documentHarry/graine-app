import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Observable } from 'rxjs';

import { Espece } from '../../services/api/models/espece.model';
import { EspeceService } from '../../services/api/espece.service';
import { AuthService } from '../../services/api/auth.service';

@Component({
  selector: 'app-especes',
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './especes.component.html',
  styleUrls: ['./especes.component.css']
})

export class EspecesComponent {
  especes$: Observable<Espece[]>;

  rechercheNomCommun = '';
  rechercheNomScientifique = '';

  constructor(
    private especeService: EspeceService,
    public authService: AuthService) {
    this.especes$ = this.especeService.getEspeces();
  }

  especesFiltrees(especes: Espece[]): Espece[] {
    const nomCommun = this.rechercheNomCommun.toLowerCase().trim();
    const nomScientifique = this.rechercheNomScientifique.toLowerCase().trim();

    return especes.filter(espece => {
      const correspondNomCommun =
        nomCommun === '' ||
        espece.nomCommun.toLowerCase().includes(nomCommun);

      const correspondNomScientifique =
        nomScientifique === '' ||
        espece.nomScientifique.toLowerCase().includes(nomScientifique);

      return correspondNomCommun && correspondNomScientifique;
    });
  }
}