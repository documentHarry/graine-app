namespace Infrastructure.Models;

public class Aromate
{
    public int IdAromate { get; set; }

    public int VarieteId { get; set; }

    public string? PartieUtilisee { get; set; }

    public string? Propriete { get; set; }

    public string? UsageCulinaire { get; set; }

    public Variete Variete { get; set; } = new();

    public List<AromatePropriete> AromateProprietes { get; set; } = [];
}