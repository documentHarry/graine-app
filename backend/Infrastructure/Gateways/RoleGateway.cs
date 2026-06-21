using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class RoleGateway : IRoleGateway
{
    private readonly IRoleRepository _roleRepository;

    public RoleGateway(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository
            ?? throw new ArgumentNullException(nameof(roleRepository));
    }

    public IEnumerable<Core.Models.Role> GetAllRoles()
    {
        var infraRoles = _roleRepository.GetAllRoles();

        var coreRoles = new List<Core.Models.Role>();

        foreach (var role in infraRoles)
        {
            coreRoles.Add(new Core.Models.Role
            {
                IdRole = role.IdRole,
                NomRole = role.NomRole
            });
        }

        return coreRoles;
    }

    public Core.Models.Role? GetRoleById(int roleId)
    {
        var infraRole = _roleRepository.GetRoleById(roleId);

        if (infraRole == null)
        {
            return null;
        }

        return new Core.Models.Role
        {
            IdRole = infraRole.IdRole,
            NomRole = infraRole.NomRole
        };
    }

    public void AddRole(Core.Models.Role role)
    {
        var infraRole = new Infrastructure.Models.Role
        {
            IdRole = role.IdRole,
            NomRole = role.NomRole
        };

        _roleRepository.AddRole(infraRole);
    }

    public void UpdateRole(Core.Models.Role role)
    {
        _roleRepository.UpdateRole(
            new Infrastructure.Models.Role
            {
                IdRole = role.IdRole,
                NomRole = role.NomRole
            });
    }

    public void DeleteRole(int roleId)
    {
        _roleRepository.DeleteRole(roleId);
    }
}