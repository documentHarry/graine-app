import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';

import { Variete } from '../../services/api/models/variete.model';
import { VarieteService } from '../../services/api/variete.service';
import { VarieteFiltresComponent } from './variete-filtres/variete-filtres.component';
import { AuthService } from '../../services/api/auth.service';

@Component({
  selector: 'app-varietes',
  imports: [CommonModule, RouterLink, VarieteFiltresComponent],
  templateUrl: './varietes.component.html',
  styleUrls: ['./varietes.component.css']
})

export class VarietesComponent {
  varietes$: Observable<Variete[]>;

  rechercheNom = '';
  bioRecherche = '';
  especeRecherche = '';
  ensoleillementRecherche = '';
  cycleVieRecherche = '';

  constructor(
    private varieteService: VarieteService,
    public authService: AuthService
  ) {
    this.varietes$ = this.varieteService.getVarietes();
  }

  varietesFiltrees(varietes: Variete[]): Variete[] {
    return this.varieteService.filtrerVarietes(
      varietes,
      this.rechercheNom,
      this.bioRecherche,
      this.especeRecherche,
      this.ensoleillementRecherche,
      this.cycleVieRecherche
    );
  }

  especesDisponibles(varietes: Variete[]): string[] {
    const especes = varietes.map(variete => variete.espece.nomCommun);

    return [...new Set(especes)].sort();
  }

  ensoleillementsDisponibles(varietes: Variete[]): string[] {
    const ensoleillements = varietes
      .map(variete => variete.typeEnsoleillement)
      .filter(ensoleillement => ensoleillement !== null);

    return [...new Set(ensoleillements)].sort();
  }

  cyclesVieDisponibles(varietes: Variete[]): string[] {
    const cycles = varietes
      .map(variete => variete.cycleDeVie)
      .filter(cycle => cycle !== null);

    return [...new Set(cycles)].sort();
  }

  getNombreProduits(variete: Variete): number {
    return this.varieteService.getNombreProduits(variete);
  }

  getLabelBio(variete: Variete): string {
    return this.varieteService.getLabelBio(variete);
  }
}