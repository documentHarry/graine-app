export interface ProduitUpdateRequest {
  idProduit: number;
  intitule: string;
  prixUnitaire: number;
  quantite: number;
  categorieId: number;
  varieteId: number;
}