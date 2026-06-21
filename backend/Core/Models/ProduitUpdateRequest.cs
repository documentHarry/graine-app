namespace Core.Models;

public class ProduitUpdateRequest
{
    public int IdProduit { get; set; }
    public string Intitule { get; set; } = string.Empty;
    public decimal PrixUnitaire { get; set; }
    public int Quantite { get; set; }
    public int CategorieId { get; set; }
    public int VarieteId { get; set; }
}