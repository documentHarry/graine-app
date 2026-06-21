import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Espece } from './models/espece.model';
import { EspeceCreateRequest } from './models/espece-create-request.model';
import { EspeceUpdateRequest } from './models/espece-update-request.model';

@Injectable({ providedIn: 'root' })
export class EspeceService {
  private apiUrl = `${environment.baseUrl}/api/especes`;

  constructor(
    private http: HttpClient
  ) { }

  getEspeces(): Observable<Espece[]> {
    return this.http.get<Espece[]>(this.apiUrl);
  }

  getEspece(id: number): Observable<Espece> {
    return this.http.get<Espece>(`${this.apiUrl}/${id}`);
  }

  addEspece(espece: EspeceCreateRequest): Observable<Espece> {
    return this.http.post<Espece>(this.apiUrl, espece);
  }

  updateEspece(id: number, espece: EspeceUpdateRequest): Observable<Espece> {
    return this.http.put<Espece>(`${this.apiUrl}/${id}`, espece);
  }

  deleteEspece(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}