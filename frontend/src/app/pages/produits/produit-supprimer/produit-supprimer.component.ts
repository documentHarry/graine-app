import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Produit } from '../../../services/api/models/produit.model';
import { ProduitService } from '../../../services/api/produit.service';

@Component({
  selector: 'app-produit-supprimer',
  imports: [CommonModule, RouterLink],
  templateUrl: './produit-supprimer.component.html',
  styleUrls: ['./produit-supprimer.component.css']
})

export class ProduitSupprimerComponent {
  produit$: Observable<Produit>;
  message: string | null = null;

  constructor(
    private produitService: ProduitService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.produit$ = this.produitService.getProduit(id);
  }

  supprimerProduit(produit: Produit): void {
    const confirmation = confirm('Voulez-vous vraiment supprimer ce produit ?');

    if (!confirmation) {
      return;
    }

    this.produitService.deleteProduit(produit.idProduit).subscribe({
      next: () => {
        this.router.navigate(['/produits']);
      },
      error: () => {
        this.message = this.produitService.getMessageErreurSuppression();
      }
    });
  }

  annuler(produit: Produit): void {
    this.router.navigate(['/produits', produit.idProduit]);
  }
}