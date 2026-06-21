using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IUtilisateurRoleRepository
{
    IEnumerable<UtilisateurRole> GetUtilisateurRoles(int utilisateurId);
    void UpdateUtilisateurRoles(int utilisateurId, IEnumerable<int> rolesIds);
}