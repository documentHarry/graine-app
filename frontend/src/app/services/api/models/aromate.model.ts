import { Variete } from './variete.model';
import { AromatePropriete } from './aromate-propriete.model';

export interface Aromate {
  idAromate: number;
  varieteId: number;
  partieUtilisee: string | null;
  propriete: string | null;
  usageCulinaire: string | null;
  variete: Variete;
  aromateProprietes: AromatePropriete[];
}