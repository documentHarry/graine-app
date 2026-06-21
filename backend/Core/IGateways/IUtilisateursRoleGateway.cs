using Core.Models;

namespace Core.IGateways;

public interface IUtilisateurRoleGateway
{
    IEnumerable<UtilisateurRole> GetUtilisateurRoles(int utilisateurId);
    void UpdateUtilisateurRoles(int utilisateurId, IEnumerable<int> rolesIds);
}