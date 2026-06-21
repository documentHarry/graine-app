import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';

import { Utilisateur } from '../../services/api/models/utilisateur.model';
import { UtilisateurService } from '../../services/api/utilisateur.service';
import { UtilisateurFiltresComponent } from './utilisateur-filtres/utilisateur-filtres.component';

@Component({
  selector: 'app-utilisateurs',
  imports: [CommonModule, RouterLink, UtilisateurFiltresComponent],
  templateUrl: './utilisateurs.component.html',
  styleUrls: ['./utilisateurs.component.css']
})

export class UtilisateursComponent {
  utilisateurs$: Observable<Utilisateur[]>;

  nomRecherche = '';
  prenomRecherche = '';
  emailRecherche = '';
  statutRecherche = '';
  roleRecherche = '';
  adresseRecherche = '';

  constructor( private utilisateurService: UtilisateurService) {
    this.utilisateurs$ = this.utilisateurService.getUtilisateurs();
  }

  utilisateursFiltres(utilisateurs: Utilisateur[]): Utilisateur[] {
    return this.utilisateurService.filtrerUtilisateurs(
      utilisateurs,
      this.nomRecherche,
      this.prenomRecherche,
      this.emailRecherche,
      this.statutRecherche,
      this.roleRecherche,
      this.adresseRecherche
    );
  }

  rolesDisponibles(utilisateurs: Utilisateur[]): string[] {
    const roles = utilisateurs
      .flatMap(utilisateur => utilisateur.utilisateurRoles ?? [])
      .map(utilisateurRole => utilisateurRole.role.nomRole);

    return [...new Set(roles)].sort();
  }

  getRolesUtilisateur(utilisateur: Utilisateur): string {
    return this.utilisateurService.getRolesUtilisateur(utilisateur);
  }

  getNomComplet(utilisateur: Utilisateur): string {
    return this.utilisateurService.getNomComplet(utilisateur);
  }

  getStatutUtilisateur(utilisateur: Utilisateur): string {
    return this.utilisateurService.getStatutUtilisateur(utilisateur);
  }

  getNombreAdresses(utilisateur: Utilisateur): number {
    return this.utilisateurService.getNombreAdresses(utilisateur);
  }
}