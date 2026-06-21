export interface LoginResponse {
  idUtilisateur: number;
  nom: string;
  prenom: string;
  email: string;
  roles: string[];
}