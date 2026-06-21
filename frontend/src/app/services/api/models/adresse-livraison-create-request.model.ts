export interface AdresseLivraisonCreateRequest {
  rue: string;
  numero: string;
  parDefaut: number;
  utilisateurId: number;
  localiteId: number;
}