export interface AromateUpdateRequest {
  idAromate: number;
  varieteId: number;
  partieUtilisee: string | null;
  propriete: string | null;
  usageCulinaire: string | null;
  proprietesIds: number[];
}