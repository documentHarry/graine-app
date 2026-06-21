import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Role } from './models/role.model';
import { Utilisateur } from './models/utilisateur.model';
import { UtilisateurRole } from './models/utilisateur-role.model';

@Injectable({ providedIn: 'root' })
export class UtilisateurRoleService {
  private apiUrl = `${environment.baseUrl}/api/utilisateurs-roles`;

  constructor(
    private http: HttpClient
  ) { }

  getUtilisateurRoles(utilisateurId: number): Observable<UtilisateurRole[]> {
    return this.http.get<UtilisateurRole[]>(`${this.apiUrl}/${utilisateurId}`);
  }

  updateUtilisateurRoles(utilisateurId: number, rolesIds: number[]): Observable<UtilisateurRole[]> {
    return this.http.put<UtilisateurRole[]>(`${this.apiUrl}/${utilisateurId}`, {
      utilisateurId,
      rolesIds
    });
  }

  getRoleIdsDepuisUtilisateurRoles(utilisateurRoles: UtilisateurRole[]): number[] {
    return utilisateurRoles.map(utilisateurRole => utilisateurRole.roleId);
  }

  isRoleCoche(role: Role, rolesIds: number[]): boolean {
    return rolesIds.includes(role.idRole);
  }

  ajouterRoleId(rolesIds: number[], role: Role): number[] {
    if (rolesIds.includes(role.idRole)) {
      return rolesIds;
    }

    return [...rolesIds, role.idRole];
  }

  retirerRoleId(rolesIds: number[], role: Role): number[] {
    return rolesIds.filter(id => id !== role.idRole);
  }

  getNomComplet(utilisateur: Utilisateur | null): string {
    if (!utilisateur) {
      return '';
    }

    return `${utilisateur.prenom} ${utilisateur.nom}`;
  }

  getMessageErreurAucunRole(): string {
    return 'Veuillez sélectionner au moins un rôle.';
  }

  getMessageErreurModification(): string {
    return 'Une erreur est survenue pendant la modification des rôles.';
  }
}