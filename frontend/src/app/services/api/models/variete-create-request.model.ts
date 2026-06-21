export interface VarieteCreateRequest {
  especeId: number;
  nom: string;
  descriptif: string | null;
  bio: number;
  cycleJours: number | null;
  couleurLegume: string | null;
  tailleFixeLegume: number | null;
  tailleMinLegume: number | null;
  tailleMaxLegume: number | null;
  espacementEntreLesPlants: number | null;
  espacementEntreLesLignes: number | null;
  typeEnsoleillement: string | null;
  typeFeuillage: string | null;
  hauteurAdulteMin: number | null;
  hauteurAdulteMax: number | null;
  dureeDeGermination: string | null;
  temperatureMinDeGermination: number | null;
  cycleDeVie: string | null;
  rusticitePlante: string | null;
  dateSemisMin: string | null;
  dateSemisMax: string | null;
  dureeAvantRecolte: string | null;
  typeDeSol: string | null;
  conseilPlantation: string | null;
}