import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Aromate } from './models/aromate.model';
import { AromateCreateRequest } from './models/aromate-create-request.model';
import { AromateUpdateRequest } from './models/aromate-update-request.model';

@Injectable({ providedIn: 'root'})
export class AromateService {
  private apiUrl = `${environment.baseUrl}/api/aromates`;

  constructor(
    private http: HttpClient
  ) { }

  getAromates(): Observable<Aromate[]> {
    return this.http.get<Aromate[]>(this.apiUrl);
  }

  getAromate(id: number): Observable<Aromate> {
    return this.http.get<Aromate>(`${this.apiUrl}/${id}`);
  }

  addAromate(aromate: AromateCreateRequest): Observable<Aromate> {
    return this.http.post<Aromate>(this.apiUrl, aromate);
  }

  updateAromate(id: number, aromate: AromateUpdateRequest): Observable<Aromate> {
    return this.http.put<Aromate>(`${this.apiUrl}/${id}`, aromate);
  }

  deleteAromate(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getProprietesMedicinales(aromate: Aromate): string[] {
    return aromate.aromateProprietes
      ?.map(item => item.proprieteMedicinale.nomPropriete)
      ?? [];
  }

  getProprietesSelectionneesDepuisAromate(aromate: Aromate | null): number[] {
    return aromate?.aromateProprietes
      ?.map(item => item.proprieteId)
      ?? [];
  }

  filtrerAromates(
    aromates: Aromate[],
    rechercheNom: string,
    especeRecherche: string,
    partieUtiliseeRecherche: string,
    usageCulinaireRecherche: string
  ): Aromate[] {
    const recherche = rechercheNom.toLowerCase().trim();

    return aromates.filter(aromate => {
      const correspondNom = recherche === '' || aromate.variete?.nom.toLowerCase().includes(recherche);
      const correspondEspece = especeRecherche === '' || aromate.variete?.espece?.nomCommun === especeRecherche;
      const correspondPartieUtilisee = partieUtiliseeRecherche === '' || aromate.partieUtilisee === partieUtiliseeRecherche;
      const correspondUsageCulinaire = usageCulinaireRecherche === '' || aromate.usageCulinaire === usageCulinaireRecherche;

      return correspondNom && correspondEspece && correspondPartieUtilisee && correspondUsageCulinaire;
    });
  }

  getMessageErreurCreation(): string {
    return 'Une erreur est survenue pendant la création de l’aromate.';
  }

  getMessageErreurModification(): string {
    return 'Une erreur est survenue pendant la modification de l’aromate.';
  }

  getMessageErreurSuppression(): string {
    return 'Une erreur est survenue pendant la suppression de l’aromate.';
  }
}