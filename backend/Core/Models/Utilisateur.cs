namespace Core.Models;

public class Utilisateur
{
    public int IdUtilisateur { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime? DateInscription { get; set; }
    public int Actif { get; set; }
    public List<AdresseLivraison> AdressesLivraison { get; set; } = [];
    public List<UtilisateurRole> UtilisateurRoles { get; set; } = [];
}