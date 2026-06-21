import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

import { environment } from '../../../environments/environment';

import { AuthenticationRequest } from './models/authentication-request.model';
import { LoginResponse } from './models/login-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.baseUrl}/api/auth`;

  private readonly DUREE_INACTIVITE = 90 * 60 * 1000;

  private utilisateur: LoginResponse | null = null;
  private timerInactivite: ReturnType<typeof setTimeout> | null = null;

  constructor(
    private http: HttpClient
  ) { }

  login(identifiants: AuthenticationRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, identifiants).pipe(
      tap(utilisateur => {
        this.setUtilisateur(utilisateur);
      })
    );
  }

  setUtilisateur(utilisateur: LoginResponse): void {
    this.utilisateur = utilisateur;
    sessionStorage.setItem('utilisateur', JSON.stringify(utilisateur));
    this.demarrerSurveillanceInactivite();
  }

  getUtilisateur(): LoginResponse | null {
    if (this.utilisateur) {
      return this.utilisateur;
    }

    const utilisateurJson = sessionStorage.getItem('utilisateur');

    if (!utilisateurJson) {
      return null;
    }

    this.utilisateur = JSON.parse(utilisateurJson);
    this.demarrerSurveillanceInactivite();

    return this.utilisateur;
  }

  isLoggedIn(): boolean {
    return this.getUtilisateur() !== null;
  }

  hasRole(role: string): boolean {
    const utilisateur = this.getUtilisateur();

    return utilisateur?.roles.includes(role) ?? false;
  }

  hasAnyRole(roles: string[]): boolean {
    const utilisateur = this.getUtilisateur();

    if (!utilisateur) {
      return false;
    }

    return roles.some(role => utilisateur.roles.includes(role));
  }

  logout(): void {
    this.utilisateur = null;
    sessionStorage.removeItem('utilisateur');

    if (this.timerInactivite) {
      clearTimeout(this.timerInactivite);
      this.timerInactivite = null;
    }
  }

  construireIdentifiantsConnexion(valeurFormulaire: {
    email: string | null;
    motDePasse: string | null;
  }): AuthenticationRequest {
    return {
      email: valeurFormulaire.email?.trim() ?? '',
      motDePasse: valeurFormulaire.motDePasse ?? ''
    };
  }

  getMessageErreurFormulaireConnexion(): string {
    return 'Veuillez remplir correctement les champs.';
  }

  getMessageErreurIdentifiants(): string {
    return 'Email ou mot de passe incorrect.';
  }

  getMessageErreurConnexion(): string {
    return 'Une erreur est survenue pendant la connexion.';
  }

  private demarrerSurveillanceInactivite(): void {
    this.reinitialiserTimerInactivite();

    window.addEventListener('mousemove', this.reinitialiserTimerInactivite);
    window.addEventListener('keydown', this.reinitialiserTimerInactivite);
    window.addEventListener('click', this.reinitialiserTimerInactivite);
  }

  private reinitialiserTimerInactivite = (): void => {
    if (!this.utilisateur) {
      return;
    }

    if (this.timerInactivite) {
      clearTimeout(this.timerInactivite);
    }

    this.timerInactivite = setTimeout(() => {
      this.logout();
    }, this.DUREE_INACTIVITE);
  };
}