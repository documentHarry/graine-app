using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IAuthRepository
{
    AuthUtilisateur? GetUtilisateurByEmail(string email);
    IEnumerable<string> GetRolesUtilisateur(int utilisateurId);
}