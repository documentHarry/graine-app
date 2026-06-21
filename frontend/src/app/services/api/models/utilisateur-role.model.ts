import { Role } from './role.model';

export interface UtilisateurRole {
  utilisateurId: number;
  roleId: number;

  role: Role;
}