namespace Core.Models;

public class AdresseLivraisonCreateRequest
{
    public string Rue { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public int ParDefaut { get; set; }
    public int UtilisateurId { get; set; }
    public int LocaliteId { get; set; }
}