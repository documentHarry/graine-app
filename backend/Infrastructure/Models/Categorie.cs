namespace Infrastructure.Models;

public class Categorie
{
    public int IdCategorie { get; set; }

    public string NomCategorie { get; set; } = string.Empty;

    public string? Descriptif { get; set; }

    public int NombreProduits { get; set; }
}