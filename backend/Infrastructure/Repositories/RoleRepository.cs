using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class RoleRepository(IDbConnectionFactory connectionFactory) : IRoleRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public IEnumerable<Role> GetAllRoles()
    {
        const string sql = """
            SELECT
                IdRole,
                NomRole
            FROM Role
            ORDER BY NomRole;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.Query<Role>(sql);
    }

    public Role? GetRoleById(int roleId)
    {
        const string sql = """
            SELECT
                IdRole,
                NomRole
            FROM Role
            WHERE IdRole = @IdRole;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.QuerySingleOrDefault<Role>(sql, new { IdRole = roleId });
    }

    public void AddRole(Role role)
    {
        const string sql = """
            INSERT INTO Role ( NomRole )
            VALUES ( @NomRole );
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, role);
    }

    public void UpdateRole(Role role)
    {
        const string sql = """
            UPDATE Role
            SET NomRole = @NomRole
            WHERE IdRole = @IdRole;
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, role);
    }

    public void DeleteRole(int roleId)
    {
        const string sql = """
            DELETE FROM Role
            WHERE IdRole = @IdRole;
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, new { IdRole = roleId });
    }
}