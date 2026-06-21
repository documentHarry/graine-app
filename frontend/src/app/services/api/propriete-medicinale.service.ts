import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { ProprieteMedicinale } from './models/propriete-medicinale.model';
import { ProprieteMedicinaleCreateRequest } from './models/propriete-medicinale-create-request.model';
import { ProprieteMedicinaleUpdateRequest } from './models/propriete-medicinale-update-request.model';

@Injectable({ providedIn: 'root' })
export class ProprieteMedicinaleService {
  private apiUrl = `${environment.baseUrl}/api/proprietes-medicinales`;

  constructor(
    private http: HttpClient
  ) { }

  getProprietesMedicinales(): Observable<ProprieteMedicinale[]> {
    return this.http.get<ProprieteMedicinale[]>(this.apiUrl);
  }

  getProprieteMedicinale(id: number): Observable<ProprieteMedicinale> {
    return this.http.get<ProprieteMedicinale>(
      `${this.apiUrl}/${id}`
    );
  }

  addProprieteMedicinale(propriete: ProprieteMedicinaleCreateRequest): Observable<ProprieteMedicinale> {
    return this.http.post<ProprieteMedicinale>(
      this.apiUrl,
      propriete
    );
  }

  updateProprieteMedicinale(id: number, propriete: ProprieteMedicinaleUpdateRequest): Observable<ProprieteMedicinale> {
    return this.http.put<ProprieteMedicinale>(
      `${this.apiUrl}/${id}`,
      propriete
    );
  }

  deleteProprieteMedicinale(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${id}`
    );
  }

  filtrerProprietesMedicinales(proprietes: ProprieteMedicinale[], recherche: string): ProprieteMedicinale[] {
    const rechercheNettoyee = recherche.toLowerCase().trim();

    return proprietes.filter(propriete => {
      return rechercheNettoyee === '' ||
        propriete.nomPropriete.toLowerCase().includes(rechercheNettoyee);
    });
  }

  getMessageErreurEnregistrement(): string {
    return 'Erreur pendant l’enregistrement de la propriété médicinale.';
  }

  getMessageErreurSuppression(): string {
    return 'Erreur pendant la suppression de la propriété médicinale.';
  }
}