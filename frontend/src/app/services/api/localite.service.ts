import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Localite } from './models/localite.model';
import { LocaliteCreateRequest } from './models/localite-create-request.model';
import { LocaliteUpdateRequest } from './models/localite-update-request.model';

@Injectable({ providedIn: 'root' })
export class LocaliteService {
  private apiUrl = `${environment.baseUrl}/api/localites`;

  constructor(
    private http: HttpClient
  ) { }

  getLocalites(): Observable<Localite[]> {
    return this.http.get<Localite[]>(this.apiUrl);
  }

  addLocalite(localite: LocaliteCreateRequest): Observable<Localite> {
    return this.http.post<Localite>(this.apiUrl, localite);
  }

  updateLocalite(id: number, localite: LocaliteUpdateRequest): Observable<Localite> {
    return this.http.put<Localite>(`${this.apiUrl}/${id}`, localite);
  }

  deleteLocalite(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}