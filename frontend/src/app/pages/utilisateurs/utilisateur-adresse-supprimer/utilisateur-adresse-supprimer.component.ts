import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { AdresseLivraison } from '../../../services/api/models/adresse-livraison.model';
import { AdresseLivraisonService } from '../../../services/api/adresse-livraison.service';

import { Utilisateur } from '../../../services/api/models/utilisateur.model';
import { UtilisateurService } from '../../../services/api/utilisateur.service';

@Component({
  selector: 'app-utilisateur-adresse-supprimer',
  imports: [CommonModule, RouterLink],
  templateUrl: './utilisateur-adresse-supprimer.component.html',
  styleUrls: ['./utilisateur-adresse-supprimer.component.css']
})
export class UtilisateurAdresseSupprimerComponent {
  utilisateur$: Observable<Utilisateur>;

  adresse: AdresseLivraison | null = null;
  message: string | null = null;

  constructor(
    private adresseLivraisonService: AdresseLivraisonService,
    private utilisateurService: UtilisateurService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const idUtilisateur = Number(this.route.snapshot.paramMap.get('id'));
    const idAdresse = Number(this.route.snapshot.paramMap.get('adresseId'));

    this.utilisateur$ = this.utilisateurService.getUtilisateur(idUtilisateur);

    this.utilisateur$.subscribe(utilisateur => {
      this.adresse = utilisateur.adressesLivraison.find(
        adresse => adresse.idAdresse === idAdresse
      ) ?? null;

      if (this.adresse === null) {
        this.message = 'Adresse introuvable.';
      }
    });
  }

  getAdresseComplete(adresse: AdresseLivraison): string {
    return this.adresseLivraisonService.getAdresseComplete(adresse);
  }

  getLabelAdresseParDefaut(adresse: AdresseLivraison): string {
    return this.adresseLivraisonService.getLabelAdresseParDefaut(adresse);
  }

  supprimerAdresse(utilisateur: Utilisateur, adresse: AdresseLivraison): void {
    const confirmation = confirm('Voulez-vous vraiment supprimer cette adresse ?');

    if (!confirmation) {
      return;
    }

    this.adresseLivraisonService.deleteAdresseLivraison(adresse.idAdresse).subscribe({
      next: () => {
        this.router.navigate(['/utilisateurs', utilisateur.idUtilisateur]);
      },
      error: () => {
        this.message = this.adresseLivraisonService.getMessageErreurSuppression();
      }
    });
  }

  annuler(utilisateur: Utilisateur): void {
    this.router.navigate(['/utilisateurs', utilisateur.idUtilisateur]);
  }
}