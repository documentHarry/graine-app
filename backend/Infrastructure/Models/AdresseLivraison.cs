namespace Infrastructure.Models;

public class AdresseLivraison
{
    public int IdAdresse { get; set; }

    public string Rue { get; set; } = string.Empty;

    public string Numero { get; set; } = string.Empty;

    public int ParDefaut { get; set; }

    public int UtilisateurId { get; set; }

    public int LocaliteId { get; set; }

    public Localite Localite { get; set; } = new();
}