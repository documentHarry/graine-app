import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Categorie } from './models/categorie.model';
import { CategorieCreateRequest } from './models/categorie-create-request.model';
import { CategorieUpdateRequest } from './models/categorie-update-request.model';

@Injectable({ providedIn: 'root' })
export class CategorieService {

  private apiUrl = `${environment.baseUrl}/api/categories`;

  constructor( private http: HttpClient ) { }

  getCategories(): Observable<Categorie[]> {
    return this.http.get<Categorie[]>(
      this.apiUrl
    );
  }

  getCategorie(id: number): Observable<Categorie> {
    return this.http.get<Categorie>(
      `${this.apiUrl}/${id}`
    );
  }

  addCategorie(categorie: CategorieCreateRequest): Observable<Categorie> {
    return this.http.post<Categorie>(this.apiUrl, categorie);
  }

  updateCategorie(id: number, categorie: CategorieUpdateRequest): Observable<Categorie> {
    return this.http.put<Categorie>(`${this.apiUrl}/${id}`, categorie);
  }

  deleteCategorie(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  deleteCategorieWithReaffectation(idCategorieASupprimer: number, idCategorieDestination: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${idCategorieASupprimer}/reaffectation/${idCategorieDestination}`
    );
  }
}