import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { AdresseLivraison } from '../../../services/api/models/adresse-livraison.model';
import { Utilisateur } from '../../../services/api/models/utilisateur.model';
import { UtilisateurService } from '../../../services/api/utilisateur.service';
import { UtilisateurAdressesComponent } from '../utilisateur-adresses/utilisateur-adresses.component';

@Component({
  selector: 'app-utilisateur-detail',
  imports: [CommonModule, RouterLink, UtilisateurAdressesComponent],
  templateUrl: './utilisateur-detail.component.html',
  styleUrls: ['./utilisateur-detail.component.css']
})

export class UtilisateurDetailComponent {
  utilisateur$: Observable<Utilisateur>;
  message: string | null = null;

  constructor(
    private utilisateurService: UtilisateurService,
    private route: ActivatedRoute
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.utilisateur$ = this.utilisateurService.getUtilisateur(id);
  }

  getStatutUtilisateur(utilisateur: Utilisateur): string {
    return this.utilisateurService.getStatutUtilisateur(utilisateur);
  }

  getNomComplet(utilisateur: Utilisateur): string {
    return this.utilisateurService.getNomComplet(utilisateur);
  }

  getRolesUtilisateur(utilisateur: Utilisateur): string {
    return this.utilisateurService.getRolesUtilisateur(utilisateur);
  }

  getAdresses(utilisateur: Utilisateur): AdresseLivraison[] {
    return utilisateur.adressesLivraison ?? [];
  }
}