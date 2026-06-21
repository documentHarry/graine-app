export interface AromateCreateRequest {
  varieteId: number;
  partieUtilisee: string | null;
  propriete: string | null;
  usageCulinaire: string | null;
  proprietesIds: number[];
}