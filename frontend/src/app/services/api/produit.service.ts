import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Produit } from './models/produit.model';
import { ProduitCreateRequest } from './models/produit-create-request.model';
import { ProduitUpdateRequest } from './models/produit-update-request.model';

@Injectable({ providedIn: 'root' })
export class ProduitService {
  private apiUrl = `${environment.baseUrl}/api/produits`;

  constructor(
    private http: HttpClient
  ) { }

  getProduits(): Observable<Produit[]> {
    return this.http.get<Produit[]>(this.apiUrl);
  }

  getProduit(id: number): Observable<Produit> {
    return this.http.get<Produit>(`${this.apiUrl}/${id}`);
  }

  getProduitsByCategorie(categorieId: number): Observable<Produit[]> {
    return this.http.get<Produit[]>(`${this.apiUrl}/categorie/${categorieId}`);
  }

  addProduit(produit: ProduitCreateRequest): Observable<Produit> {
    return this.http.post<Produit>(this.apiUrl, produit);
  }

  updateProduit(id: number, produit: ProduitUpdateRequest): Observable<Produit> {
    return this.http.put<Produit>(`${this.apiUrl}/${id}`, produit);
  }

  deleteProduit(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  filtrerProduits(
    produits: Produit[],
    rechercheTexte: string,
    stockRecherche: string,
    prixMinRecherche: string,
    prixMaxRecherche: string,
    varieteRecherche: string,
    especeRecherche: string
  ): Produit[] {
    const recherche = rechercheTexte.toLowerCase().trim();

    return produits.filter(produit => {
      const correspondRecherche =
        recherche === '' ||
        produit.intitule.toLowerCase().includes(recherche) ||
        produit.variete.nom.toLowerCase().includes(recherche) ||
        produit.variete.espece.nomCommun.toLowerCase().includes(recherche) ||
        produit.variete.espece.nomScientifique.toLowerCase().includes(recherche);

      const correspondStock =
        stockRecherche === '' ||
        (stockRecherche === 'en-stock' && produit.quantite > 0) ||
        (stockRecherche === 'rupture' && produit.quantite === 0);

      const prixMin = prixMinRecherche === '' ? null : Number(prixMinRecherche);
      const prixMax = prixMaxRecherche === '' ? null : Number(prixMaxRecherche);

      const correspondPrixMin =
        prixMin === null ||
        produit.prixUnitaire >= prixMin;

      const correspondPrixMax =
        prixMax === null ||
        produit.prixUnitaire <= prixMax;

      const correspondVariete =
        varieteRecherche === '' ||
        produit.variete.nom === varieteRecherche;

      const correspondEspece =
        especeRecherche === '' ||
        produit.variete.espece.nomCommun === especeRecherche;

      return correspondRecherche && correspondStock && correspondPrixMin && correspondPrixMax &&
        correspondVariete && correspondEspece;
    });
  }

  getStatutProduit(produit: Produit | null): string {
    if (produit?.quantite && produit.quantite > 0) {
      return 'En stock';
    }

    return 'Rupture de stock';
  }

  getLabelBio(produit: Produit | null): string {
    if (produit?.variete?.bio === 1) {
      return 'Oui';
    }

    return 'Non';
  }

  getImageProduit(produit: Produit | null): string | null {
    return produit?.imageProduit ?? null;
  }

  getMessageErreurCreation(): string {
    return 'Une erreur est survenue pendant la création du produit.';
  }

  getMessageErreurModification(): string {
    return 'Une erreur est survenue pendant la modification du produit.';
  }

  getMessageErreurSuppression(): string {
    return 'Une erreur est survenue pendant la suppression du produit.';
  }
}