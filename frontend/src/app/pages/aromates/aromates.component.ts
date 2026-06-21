import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';

import { Aromate } from '../../services/api/models/aromate.model';
import { AromateService } from '../../services/api/aromate.service';
import { AromateFiltresComponent } from './aromate-filtres/aromate-filtres.component';
import { AuthService } from '../../services/api/auth.service';

@Component({
  selector: 'app-aromates',
  imports: [CommonModule, RouterLink, AromateFiltresComponent],
  templateUrl: './aromates.component.html',
  styleUrls: ['./aromates.component.css']
})

export class AromatesComponent {
  aromates$: Observable<Aromate[]>;

  rechercheNom = '';
  especeRecherche = '';
  partieUtiliseeRecherche = '';
  usageCulinaireRecherche = '';

  constructor(
    private aromateService: AromateService,
    public authService: AuthService) {
    this.aromates$ = this.aromateService.getAromates();
  }

  aromatesFiltres(aromates: Aromate[]): Aromate[] {
    return this.aromateService.filtrerAromates(
      aromates,
      this.rechercheNom,
      this.especeRecherche,
      this.partieUtiliseeRecherche,
      this.usageCulinaireRecherche
    );
  }

  especesDisponibles(aromates: Aromate[]): string[] {
    const especes = aromates
      .map(aromate => aromate.variete?.espece?.nomCommun)
      .filter(espece => espece !== undefined);

    return [...new Set(especes)].sort();
  }

  partiesUtiliseesDisponibles(aromates: Aromate[]): string[] {
    const parties = aromates
      .map(aromate => aromate.partieUtilisee)
      .filter(partie => partie !== null);

    return [...new Set(parties)].sort();
  }

  usagesCulinairesDisponibles(aromates: Aromate[]): string[] {
    const usages = aromates
      .map(aromate => aromate.usageCulinaire)
      .filter(usage => usage !== null);

    return [...new Set(usages)].sort();
  }

  getProprietesMedicinales(aromate: Aromate): string[] {
    return this.aromateService.getProprietesMedicinales(aromate);
  }
}