using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class AuthRepository(IDbConnectionFactory connectionFactory) : IAuthRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public AuthUtilisateur? GetUtilisateurByEmail(string email)
    {
        const string sql = """
            SELECT
                IdUtilisateur,
                Nom,
                Prenom,
                Email,
                MotDePasseHash,
                MotDePasseSalt
            FROM Utilisateur
            WHERE Email = @Email
              AND Actif = 1;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.QuerySingleOrDefault<AuthUtilisateur>( sql, new { Email = email });
    }

    public IEnumerable<string> GetRolesUtilisateur(int utilisateurId)
    {
        const string sql = """
            SELECT
                r.NomRole
            FROM UtilisateurRole ur
            INNER JOIN Role r
                ON r.IdRole = ur.RoleId
            WHERE ur.UtilisateurId = @UtilisateurId
            ORDER BY r.NomRole;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.Query<string>(sql, new { UtilisateurId = utilisateurId });
    }
}