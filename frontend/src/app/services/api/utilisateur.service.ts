import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Utilisateur } from './models/utilisateur.model';
import { UtilisateurCreateRequest } from './models/utilisateur-create-request.model';
import { UtilisateurUpdateRequest } from './models/utilisateur-update-request.model';

@Injectable({ providedIn: 'root' })
export class UtilisateurService {
  private apiUrl = `${environment.baseUrl}/api/utilisateurs`;

  constructor(
    private http: HttpClient
  ) { }

  getUtilisateurs(): Observable<Utilisateur[]> {
    return this.http.get<Utilisateur[]>(this.apiUrl);
  }

  getUtilisateur(id: number): Observable<Utilisateur> {
    return this.http.get<Utilisateur>(`${this.apiUrl}/${id}`);
  }

  addUtilisateur(utilisateur: UtilisateurCreateRequest): Observable<Utilisateur> {
    return this.http.post<Utilisateur>(this.apiUrl, utilisateur);
  }

  updateUtilisateur(id: number, utilisateur: UtilisateurUpdateRequest): Observable<Utilisateur> {
    return this.http.put<Utilisateur>(`${this.apiUrl}/${id}`, utilisateur);
  }

  deleteUtilisateur(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  filtrerUtilisateurs(
    utilisateurs: Utilisateur[],
    nomRecherche: string,
    prenomRecherche: string,
    emailRecherche: string,
    statutRecherche: string,
    roleRecherche: string,
    adresseRecherche: string
  ): Utilisateur[] {
    const nom = nomRecherche.toLowerCase().trim();
    const prenom = prenomRecherche.toLowerCase().trim();
    const email = emailRecherche.toLowerCase().trim();

    return utilisateurs.filter(utilisateur => {
      const correspondNom = nom === '' || utilisateur.nom.toLowerCase().includes(nom);
      const correspondPrenom = prenom === '' || utilisateur.prenom.toLowerCase().includes(prenom);
      const correspondEmail = email === '' || utilisateur.email.toLowerCase().includes(email);

      const correspondStatut =
        statutRecherche === '' ||
        (statutRecherche === 'actif' && utilisateur.actif === 1) ||
        (statutRecherche === 'inactif' && utilisateur.actif === 0);

      const correspondRole =
        roleRecherche === '' ||
        (utilisateur.utilisateurRoles ?? [])
          .some(utilisateurRole => utilisateurRole.role.nomRole === roleRecherche);

      const nombreAdresses = this.getNombreAdresses(utilisateur);

      const correspondAdresse =
        adresseRecherche === '' ||
        (adresseRecherche === 'avec-adresse' && nombreAdresses > 0) ||
        (adresseRecherche === 'sans-adresse' && nombreAdresses === 0);

      return correspondNom &&
        correspondPrenom &&
        correspondEmail &&
        correspondStatut &&
        correspondRole &&
        correspondAdresse;
    });
  }

  getRolesUtilisateur(utilisateur: Utilisateur | null): string {
    const roles = utilisateur?.utilisateurRoles ?? [];

    if (roles.length === 0) {
      return 'Aucun rôle';
    }

    return roles
      .map(utilisateurRole => utilisateurRole.role.nomRole)
      .join(', ');
  }

  getNomComplet(utilisateur: Utilisateur | null): string {
    if (!utilisateur) {
      return '';
    }

    return `${utilisateur.prenom} ${utilisateur.nom}`;
  }

  getStatutUtilisateur(utilisateur: Utilisateur | null): string {
    if (utilisateur?.actif === 1) {
      return 'Actif';
    }

    return 'Inactif';
  }

  getNombreAdresses(utilisateur: Utilisateur | null): number {
    return utilisateur?.adressesLivraison?.length ?? 0;
  }

  getMessageErreurCreation(): string {
    return 'Une erreur est survenue pendant la création de l’utilisateur.';
  }

  getMessageErreurModification(): string {
    return 'Une erreur est survenue pendant la modification de l’utilisateur.';
  }

  getMessageErreurSuppression(): string {
    return 'Une erreur est survenue pendant la suppression de l’utilisateur.';
  }
}