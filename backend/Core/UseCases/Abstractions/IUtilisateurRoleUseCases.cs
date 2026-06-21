using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IUtilisateurRoleUseCases
{
    IEnumerable<UtilisateurRole> GetUtilisateurRoles(int utilisateurId);
    void UpdateUtilisateurRoles(int utilisateurId, IEnumerable<int> rolesIds);
}