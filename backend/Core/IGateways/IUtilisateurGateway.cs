using Core.Models;

namespace Core.IGateways;

public interface IUtilisateurGateway
{
    IEnumerable<Utilisateur> GetAllUtilisateurs();
    Utilisateur? GetUtilisateurById(int utilisateurId);
    void AddUtilisateur(Utilisateur utilisateur);
    bool UpdateUtilisateur(Utilisateur utilisateur);
    bool DeleteUtilisateur(int utilisateurId);
}