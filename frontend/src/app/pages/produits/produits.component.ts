import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';

import { Produit } from '../../services/api/models/produit.model';
import { ProduitService } from '../../services/api/produit.service';
import { ProduitFiltresComponent } from './produit-filtres/produit-filtres.component';
import { AuthService } from '../../services/api/auth.service';

@Component({
  selector: 'app-produits',
  imports: [CommonModule, RouterLink, ProduitFiltresComponent],
  templateUrl: './produits.component.html',
  styleUrls: ['./produits.component.css']
})

export class ProduitsComponent {
  produits$: Observable<Produit[]>;

  categorieId: number | null = null;

  recherche = '';
  varieteRecherche = '';
  especeRecherche = '';
  stockRecherche = '';
  prixMinRecherche = '';
  prixMaxRecherche = '';

  constructor(
    private produitService: ProduitService,
    private route: ActivatedRoute,
    public authService: AuthService
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    if (id) {
      this.categorieId = id;
    }

    this.produits$ = this.categorieId
      ? this.produitService.getProduitsByCategorie(this.categorieId)
      : this.produitService.getProduits();
  }

  produitsFiltres(produits: Produit[]): Produit[] {
    return this.produitService.filtrerProduits(
      produits,
      this.recherche,
      this.stockRecherche,
      this.prixMinRecherche,
      this.prixMaxRecherche,
      this.varieteRecherche,
      this.especeRecherche
    );
  }

  getStatutProduit(produit: Produit): string {
    return this.produitService.getStatutProduit(produit);
  }

  varietesDisponibles(produits: Produit[]): string[] {
    const varietes = produits.map(produit => produit.variete.nom);

    return [...new Set(varietes)].sort();
  }

  especesDisponibles(produits: Produit[]): string[] {
    const especes = produits.map(produit => produit.variete.espece.nomCommun);

    return [...new Set(especes)].sort();
  }

  prixMinimumDisponible(produits: Produit[]): number {
    const prix = produits.map(produit => produit.prixUnitaire);

    if (prix.length === 0) {
      return 0;
    }

    return Math.min(...prix);
  }

  prixMaximumDisponible(produits: Produit[]): number {
    const prix = produits.map(produit => produit.prixUnitaire);

    if (prix.length === 0) {
      return 0;
    }

    return Math.max(...prix);
  }
}