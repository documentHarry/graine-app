import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Variete } from './models/variete.model';
import { VarieteCreateRequest } from './models/variete-create-request.model';
import { VarieteUpdateRequest } from './models/variete-update-request.model';

@Injectable({ providedIn: 'root'})
export class VarieteService {
  private apiUrl = `${environment.baseUrl}/api/varietes`;

  constructor(
    private http: HttpClient
  ) { }

  getVarietes(): Observable<Variete[]> {
    return this.http.get<Variete[]>(this.apiUrl);
  }

  getVariete(id: number): Observable<Variete> {
    return this.http.get<Variete>(`${this.apiUrl}/${id}`);
  }

  addVariete(variete: VarieteCreateRequest): Observable<Variete> {
    return this.http.post<Variete>(this.apiUrl, variete);
  }

  updateVariete(id: number, variete: VarieteUpdateRequest): Observable<Variete> {
    return this.http.put<Variete>(
      `${this.apiUrl}/${id}`,
      variete
    );
  }

  deleteVariete(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${id}`
    );
  }

  filtrerVarietes(
    varietes: Variete[],
    rechercheNom: string,
    bioRecherche: string,
    especeRecherche: string,
    ensoleillementRecherche: string,
    cycleVieRecherche: string
  ): Variete[] {
    const nom = rechercheNom.toLowerCase().trim();

    return varietes.filter(variete => {
      const correspondNom =
        nom === '' ||
        variete.nom.toLowerCase().includes(nom);

      const correspondBio =
        bioRecherche === '' ||
        (bioRecherche === 'bio' && variete.bio === 1) ||
        (bioRecherche === 'non-bio' && variete.bio !== 1);

      const correspondEspece =
        especeRecherche === '' ||
        variete.espece.nomCommun === especeRecherche;

      const correspondEnsoleillement =
        ensoleillementRecherche === '' ||
        variete.typeEnsoleillement === ensoleillementRecherche;

      const correspondCycleVie =
        cycleVieRecherche === '' ||
        variete.cycleDeVie === cycleVieRecherche;

      return ( correspondNom && correspondBio && correspondEspece &&
        correspondEnsoleillement && correspondCycleVie
      );
    });
  }

  getNombreProduits(variete: Variete | null): number {
    return variete?.nombreProduits ?? 0;
  }

  getLabelBio(variete: Variete | null): string {
    return variete?.bio === 1 ? 'Bio' : 'Non bio';
  }

  getConseilsPlantation(variete: Variete | null): string[] {
    const conseil = variete?.conseilPlantation;

    if (!conseil) {
      return [];
    }

    return conseil
      .split('.')
      .map(phrase => phrase.trim())
      .filter(phrase => phrase.length > 0)
      .map(phrase => `${phrase}.`);
  }

  getMessageErreurCreation(): string {
    return 'Une erreur est survenue pendant la création de la variété.';
  }

  getMessageErreurModification(): string {
    return 'Une erreur est survenue pendant la modification de la variété.';
  }

  getMessageErreurSuppression(): string {
    return 'Cette variété possède des produits associés. Elle ne peut pas être supprimée.';
  }
}