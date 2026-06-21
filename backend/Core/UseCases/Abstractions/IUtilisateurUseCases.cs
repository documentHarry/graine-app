using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IUtilisateurUseCases
{
    IEnumerable<Utilisateur> GetAllUtilisateurs();
    Utilisateur? GetUtilisateurById(int utilisateurId);
    void AddUtilisateur(UtilisateurCreateRequest request);
    bool UpdateUtilisateur(int utilisateurId, UtilisateurUpdateRequest request);
    bool DeleteUtilisateur(int utilisateurId);
}