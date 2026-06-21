namespace Core.Models;

public class Produit
{
    public int IdProduit { get; set; }
    public string Intitule { get; set; } = string.Empty;
    public decimal PrixUnitaire { get; set; }
    public int Quantite { get; set; }
    public string? ImageProduit { get; set; }
    public DateTime? DateAjout { get; set; }
    public int VarieteId { get; set; }
    public int CategorieId { get; set; }
    public Variete Variete { get; set; } = new();
    public Categorie Categorie { get; set; } = new();
}