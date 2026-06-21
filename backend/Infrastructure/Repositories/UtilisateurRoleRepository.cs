using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class UtilisateurRoleRepository(IDbConnectionFactory connectionFactory) : IUtilisateurRoleRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public IEnumerable<UtilisateurRole> GetUtilisateurRoles(int utilisateurId)
    {
        const string sql = """
            SELECT
                ur.UtilisateurId,
                ur.RoleId,

                r.IdRole,
                r.NomRole
            FROM UtilisateurRole ur
            INNER JOIN Role r
                ON r.IdRole = ur.RoleId
            WHERE ur.UtilisateurId = @UtilisateurId
            ORDER BY r.NomRole;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return connection.Query<UtilisateurRole, Role, UtilisateurRole>(
            sql, (utilisateurRole, role) =>
            {
                utilisateurRole.Role = role;
                return utilisateurRole;
            },
            new { UtilisateurId = utilisateurId },
            splitOn: "IdRole"
        );
    }

    public void UpdateUtilisateurRoles(int utilisateurId, IEnumerable<int> rolesIds)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        const string deleteSql = """
            DELETE FROM UtilisateurRole
            WHERE UtilisateurId = @UtilisateurId;
            """;

        connection.Execute(deleteSql, new { UtilisateurId = utilisateurId }, transaction);

        const string insertSql = """
            INSERT INTO UtilisateurRole ( UtilisateurId, RoleId )
            VALUES ( @UtilisateurId, @RoleId );
            """;

        foreach (var roleId in rolesIds)
        {
            connection.Execute(insertSql, new { UtilisateurId = utilisateurId, RoleId = roleId }, transaction);
        }
        transaction.Commit();
    }
}