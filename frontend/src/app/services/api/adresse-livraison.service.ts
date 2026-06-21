import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { AdresseLivraison } from './models/adresse-livraison.model';
import { AdresseLivraisonCreateRequest } from './models/adresse-livraison-create-request.model';
import { AdresseLivraisonUpdateRequest } from './models/adresse-livraison-update-request.model';

@Injectable({ providedIn: 'root' })
export class AdresseLivraisonService {
  private apiUrl = `${environment.baseUrl}/api/adresses-livraison`;

  constructor(
    private http: HttpClient
  ) { }

  addAdresseLivraison(adresse: AdresseLivraisonCreateRequest): Observable<AdresseLivraison> {
    return this.http.post<AdresseLivraison>(this.apiUrl, adresse);
  }

  updateAdresseLivraison(id: number, adresse: AdresseLivraisonUpdateRequest): Observable<AdresseLivraison> {
    return this.http.put<AdresseLivraison>(`${this.apiUrl}/${id}`, adresse);
  }

  deleteAdresseLivraison(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getAdresseComplete(adresse: AdresseLivraison): string {
    return `${adresse.rue} ${adresse.numero}, ${adresse.localite.codePostal} ${adresse.localite.nomLocalite}`;
  }

  getLabelAdresseParDefaut(adresse: AdresseLivraison): string {
    if (adresse.parDefaut === 1) {
      return 'Adresse par défaut';
    }

    return 'Adresse secondaire';
  }

  getMessageErreurChampsObligatoires(): string {
    return 'Veuillez remplir les champs obligatoires de l’adresse.';
  }

  getMessageErreurLocalite(): string {
    return 'Veuillez sélectionner une localité.';
  }

  getMessageErreurEnregistrement(): string {
    return 'Une erreur est survenue pendant l’enregistrement de l’adresse.';
  }

  getMessageErreurSuppression(): string {
    return 'Une erreur est survenue pendant la suppression de l’adresse.';
  }
}