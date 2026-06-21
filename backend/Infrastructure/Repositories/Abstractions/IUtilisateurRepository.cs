using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IUtilisateurRepository
{
    IEnumerable<Utilisateur> GetAllUtilisateurs();
    Utilisateur? GetUtilisateurById(int utilisateurId);
    void AddUtilisateur(Utilisateur utilisateur);
    bool UpdateUtilisateur(Utilisateur utilisateur);
    bool DeleteUtilisateur(int utilisateurId);
}