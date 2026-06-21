using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class RoleUseCases : IRoleUseCases
{
    private readonly IRoleGateway _roleGateway;

    public RoleUseCases(IRoleGateway roleGateway)
    {
        if (roleGateway is null)
        {
            throw new Exception("RoleGateway est obligatoire.");
        }

        _roleGateway = roleGateway;
    }

    public IEnumerable<Role> GetAllRoles()
    {
        return _roleGateway.GetAllRoles();
    }

    public void AddRole(Role role)
    {
        if (string.IsNullOrWhiteSpace(role.NomRole))
        {
            throw new Exception("Le nom du rôle est obligatoire.");
        }
        _roleGateway.AddRole(role);
    }

    public Role? GetRoleById(int roleId)
    {
        return _roleGateway.GetRoleById(roleId);
    }

    public void UpdateRole(Role role)
    {
        _roleGateway.UpdateRole(role);
    }

    public void DeleteRole(int roleId)
    {
        _roleGateway.DeleteRole(roleId);
    }
}