import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';

import { AdresseLivraison } from '../../../services/api/models/adresse-livraison.model';
import { AdresseLivraisonService } from '../../../services/api/adresse-livraison.service';

@Component({
  selector: 'app-utilisateur-adresses',
  imports: [RouterLink],
  templateUrl: './utilisateur-adresses.component.html',
  styleUrls: ['./utilisateur-adresses.component.css']
})

export class UtilisateurAdressesComponent {
  utilisateurId = input.required<number>();
  adresses = input<AdresseLivraison[]>([]);

  constructor(
    private adresseLivraisonService: AdresseLivraisonService
  ) { }

  getAdresseComplete(adresse: AdresseLivraison): string {
    return this.adresseLivraisonService.getAdresseComplete(adresse);
  }

  getLabelAdresseParDefaut(adresse: AdresseLivraison): string {
    return this.adresseLivraisonService.getLabelAdresseParDefaut(adresse);
  }
}