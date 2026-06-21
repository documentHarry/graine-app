using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IRoleUseCases
{
    IEnumerable<Role> GetAllRoles();
    void AddRole(Role role);
    Role? GetRoleById(int roleId);
    void UpdateRole(Role role);
    void DeleteRole(int roleId);
}