using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class UtilisateurRoleUseCases : IUtilisateurRoleUseCases
{
    private readonly IUtilisateurRoleGateway _gateway;

    public UtilisateurRoleUseCases(IUtilisateurRoleGateway gateway)
    {
        if (gateway is null)
        {
            throw new Exception("UtilisateurRoleGateway est obligatoire.");
        }
        _gateway = gateway;
    }

    public IEnumerable<UtilisateurRole> GetUtilisateurRoles(int utilisateurId)
    {
        return _gateway.GetUtilisateurRoles(utilisateurId);
    }

    public void UpdateUtilisateurRoles(int utilisateurId, IEnumerable<int> rolesIds)
    {
        _gateway.UpdateUtilisateurRoles(utilisateurId, rolesIds);
    }
}