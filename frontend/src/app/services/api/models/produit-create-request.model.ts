export interface ProduitCreateRequest {
  intitule: string;
  prixUnitaire: number;
  quantite: number;
  categorieId: number;
  varieteId: number;
}