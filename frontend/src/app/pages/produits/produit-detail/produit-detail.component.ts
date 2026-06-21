import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Produit } from '../../../services/api/models/produit.model';
import { ProduitService } from '../../../services/api/produit.service';
import { AuthService } from '../../../services/api/auth.service';

@Component({
  selector: 'app-produit-detail',
  imports: [CommonModule, RouterLink],
  templateUrl: './produit-detail.component.html',
  styleUrls: ['./produit-detail.component.css']
})

export class ProduitDetailComponent {
  produit$: Observable<Produit>;
  message: string | null = null;

  constructor(
    private produitService: ProduitService,
    private route: ActivatedRoute,
    private router: Router,
    public authService: AuthService
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.produit$ = this.produitService.getProduit(id);
  }

  getLabelBio(produit: Produit): string {
    return this.produitService.getLabelBio(produit);
  }

  getImageProduit(produit: Produit): string | null {
    return this.produitService.getImageProduit(produit);
  }

  getStatutProduit(produit: Produit): string {
    return this.produitService.getStatutProduit(produit);
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
}