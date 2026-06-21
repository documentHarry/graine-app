import { Categorie } from './categorie.model';
import { Variete } from './variete.model';

export interface Produit {
  idProduit: number;
  intitule: string;
  prixUnitaire: number;
  quantite: number;
  dateAjout: string | null;
  imageProduit: string | null;
  categorieId: number;
  varieteId: number;
  categorie: Categorie;
  variete: Variete;
}