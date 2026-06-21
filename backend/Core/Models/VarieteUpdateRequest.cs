namespace Core.Models;

public class VarieteUpdateRequest
{
    public int IdVariete { get; set; }
    public int EspeceId { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? Descriptif { get; set; }
    public int Bio { get; set; }
    public int? CycleJours { get; set; }
    public string? CouleurLegume { get; set; }
    public double? TailleFixeLegume { get; set; }
    public double? TailleMinLegume { get; set; }
    public double? TailleMaxLegume { get; set; }
    public int? EspacementEntreLesPlants { get; set; }
    public int? EspacementEntreLesLignes { get; set; }
    public string? TypeEnsoleillement { get; set; }
    public string? TypeFeuillage { get; set; }
    public int? HauteurAdulteMin { get; set; }
    public int? HauteurAdulteMax { get; set; }
    public string? DureeDeGermination { get; set; }
    public int? TemperatureMinDeGermination { get; set; }
    public string? CycleDeVie { get; set; }
    public string? RusticitePlante { get; set; }
    public string? DateSemisMin { get; set; }
    public string? DateSemisMax { get; set; }
    public string? DureeAvantRecolte { get; set; }
    public string? TypeDeSol { get; set; }
    public string? ConseilPlantation { get; set; }
}