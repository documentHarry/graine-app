export interface LoginResponse {
  token: string;
  idUtilisateur: number;
  nom: string;
  prenom: string;
  email: string;
  roles: string[];
}