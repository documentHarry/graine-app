using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class UtilisateurRepository(IDbConnectionFactory connectionFactory) : IUtilisateurRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public IEnumerable<Utilisateur> GetAllUtilisateurs()
    {
        const string sql = """
            SELECT
                IdUtilisateur,
                Nom,
                Prenom,
                Email,
                DateInscription,
                Actif
            FROM Utilisateur
            ORDER BY Nom, Prenom;
            """;

        using var connection = _connectionFactory.CreateConnection();
        var utilisateurs = connection.Query<Utilisateur>(sql).ToList();

        foreach (var utilisateur in utilisateurs)
        {
            utilisateur.AdressesLivraison = GetAdressesByUtilisateur(utilisateur.IdUtilisateur).ToList();
            utilisateur.UtilisateurRoles = GetRolesByUtilisateur(utilisateur.IdUtilisateur).ToList();
        }

        return utilisateurs;
    }

    public Utilisateur? GetUtilisateurById(int utilisateurId)
    {
        const string sql = """
            SELECT
                IdUtilisateur,
                Nom,
                Prenom,
                Email,
                DateInscription,
                Actif
            FROM Utilisateur
            WHERE IdUtilisateur = @IdUtilisateur;
            """;

        using var connection = _connectionFactory.CreateConnection();
        var utilisateur = connection.QuerySingleOrDefault<Utilisateur>(sql, new { IdUtilisateur = utilisateurId });

        if (utilisateur == null)
        {
            return null;
        }

        utilisateur.AdressesLivraison = GetAdressesByUtilisateur(utilisateur.IdUtilisateur).ToList();
        utilisateur.UtilisateurRoles = GetRolesByUtilisateur(utilisateur.IdUtilisateur).ToList();
        return utilisateur;
    }

    public void AddUtilisateur(Utilisateur utilisateur)
    {
        const string sql = """
            INSERT INTO Utilisateur (
                Nom,
                Prenom,
                Email,
                MotDePasseHash,
                MotDePasseSalt,
                Actif
            )
            VALUES (
                @Nom,
                @Prenom,
                @Email,
                '',
                0x,
                @Actif
            );
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, utilisateur);
    }

    private IEnumerable<AdresseLivraison> GetAdressesByUtilisateur(int utilisateurId)
    {
        const string sql = """
            SELECT
                a.*,

                l.IdLocalite,
                l.CodePostal,
                l.NomLocalite

            FROM AdresseLivraison a

            INNER JOIN Localite l
                ON l.IdLocalite = a.LocaliteId

            WHERE a.UtilisateurId = @UtilisateurId

            ORDER BY a.ParDefaut DESC,
                     a.Rue;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return connection.Query<AdresseLivraison, Localite, AdresseLivraison>(
            sql, (adresse, localite) =>
            {
                adresse.Localite = localite;
                return adresse;
            },
            new { UtilisateurId = utilisateurId },
            splitOn: "IdLocalite"
        );
    }

    private IEnumerable<UtilisateurRole> GetRolesByUtilisateur(int utilisateurId)
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

    public bool UpdateUtilisateur(Utilisateur utilisateur)
    {
        const string sql = """
            UPDATE Utilisateur
            SET
                Nom = @Nom,
                Prenom = @Prenom,
                Email = @Email
            WHERE IdUtilisateur = @IdUtilisateur;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesModifiees = connection.Execute(sql, utilisateur);
        return lignesModifiees > 0;
    }

    public bool DeleteUtilisateur(int utilisateurId)
    {
        const string sql = """
            UPDATE Utilisateur
            SET Actif = 0
            WHERE IdUtilisateur = @IdUtilisateur;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesModifiees = connection.Execute(sql, new { IdUtilisateur = utilisateurId });
        return lignesModifiees > 0;
    }
}