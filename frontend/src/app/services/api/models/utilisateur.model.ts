import { AdresseLivraison } from './adresse-livraison.model';
import { UtilisateurRole } from './utilisateur-role.model';

export interface Utilisateur {
  idUtilisateur: number;

  nom: string;
  prenom: string;
  email: string;

  dateInscription: string | null;
  actif: number;

  adressesLivraison: AdresseLivraison[];
  utilisateurRoles: UtilisateurRole[];
}