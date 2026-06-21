import { Localite } from './localite.model';

export interface AdresseLivraison {
  idAdresse: number;
  rue: string;
  numero: string;
  parDefaut: number;
  utilisateurId: number;
  localiteId: number;

  localite: Localite;
}