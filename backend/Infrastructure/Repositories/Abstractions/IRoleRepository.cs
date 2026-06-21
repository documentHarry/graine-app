using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IRoleRepository
{
    IEnumerable<Role> GetAllRoles();
    Role? GetRoleById(int roleId);
    void AddRole(Role role);
    void UpdateRole(Role role);
    void DeleteRole(int roleId);
}