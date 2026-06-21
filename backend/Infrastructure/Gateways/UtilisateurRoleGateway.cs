using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class UtilisateurRoleGateway : IUtilisateurRoleGateway
{
    private readonly IUtilisateurRoleRepository _repository;

    public UtilisateurRoleGateway( IUtilisateurRoleRepository repository)
    {
        _repository = repository
            ?? throw new ArgumentNullException(nameof(repository));
    }

    public IEnumerable<Core.Models.UtilisateurRole>GetUtilisateurRoles(int utilisateurId)
    {
        var infraRoles =
            _repository.GetUtilisateurRoles(utilisateurId);

        var coreRoles =
            new List<Core.Models.UtilisateurRole>();

        foreach (var utilisateurRole in infraRoles)
        {
            coreRoles.Add(
                new Core.Models.UtilisateurRole
                {
                    UtilisateurId = utilisateurRole.UtilisateurId,
                    RoleId = utilisateurRole.RoleId,

                    Role = new Core.Models.Role
                    {
                        IdRole = utilisateurRole.Role.IdRole,
                        NomRole = utilisateurRole.Role.NomRole
                    }
                });
        }

        return coreRoles;
    }

    public void UpdateUtilisateurRoles(int utilisateurId, IEnumerable<int> rolesIds)
    {
        _repository.UpdateUtilisateurRoles(utilisateurId, rolesIds);
    }
}