using Core.Models;

namespace Core.IGateways;

public interface IRoleGateway
{
    IEnumerable<Role> GetAllRoles();
    Role? GetRoleById(int roleId);
    void AddRole(Role role);
    void UpdateRole(Role role);
    void DeleteRole(int roleId);
}