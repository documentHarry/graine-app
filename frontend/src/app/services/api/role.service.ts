import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Role } from './models/role.model';
import { RoleCreateRequest } from './models/role-create-request.model';
import { RoleUpdateRequest } from './models/role-update-request.model';

@Injectable({ providedIn: 'root' })
export class RoleService {
  private apiUrl = `${environment.baseUrl}/api/roles`;

  constructor(private http: HttpClient) { }

  getRoles(): Observable<Role[]> {
    return this.http.get<Role[]>(this.apiUrl);
  }

  getRole(id: number): Observable<Role> {
    return this.http.get<Role>(`${this.apiUrl}/${id}`);
  }

  addRole(role: RoleCreateRequest): Observable<Role> {
    return this.http.post<Role>(this.apiUrl, role);
  }

  updateRole(id: number, role: RoleUpdateRequest): Observable<Role> {
    return this.http.put<Role>(`${this.apiUrl}/${id}`, role);
  }

  deleteRole(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  filtrerRoles(roles: Role[], recherche: string): Role[] {
    const rechercheNettoyee = recherche.toLowerCase().trim();

    return roles.filter(role =>
      rechercheNettoyee === '' ||
      role.nomRole.toLowerCase().includes(rechercheNettoyee)
    );
  }

  getMessageErreurCreationRole(): string {
    return 'Une erreur est survenue pendant la création du rôle.';
  }

  getMessageErreurModificationRole(): string {
    return 'Une erreur est survenue pendant la modification du rôle.';
  }

  getMessageErreurSuppressionRole(): string {
    return 'Une erreur est survenue pendant la suppression du rôle.';
  }
}