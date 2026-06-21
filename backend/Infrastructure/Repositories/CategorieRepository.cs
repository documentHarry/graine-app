using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class CategorieRepository(IDbConnectionFactory connectionFactory) : ICategorieRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public IEnumerable<Categorie> GetAllCategories()
    {
        const string sql = """
            SELECT
                c.IdCategorie,
                c.NomCategorie,
                c.Descriptif,
                COUNT(p.IdProduit) AS NombreProduits
            FROM Categorie c
            LEFT JOIN Produit p ON p.CategorieId = c.IdCategorie
            GROUP BY
                c.IdCategorie,
                c.NomCategorie,
                c.Descriptif
            ORDER BY c.NomCategorie;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.Query<Categorie>(sql);
    }

    public Categorie? GetCategorieById(int categorieId)
    {
        const string sql = """
            SELECT
                c.IdCategorie,
                c.NomCategorie,
                c.Descriptif,
                COUNT(p.IdProduit) AS NombreProduits
            FROM Categorie c
            LEFT JOIN Produit p ON p.CategorieId = c.IdCategorie
            WHERE c.IdCategorie = @IdCategorie
            GROUP BY
                c.IdCategorie,
                c.NomCategorie,
                c.Descriptif;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.QuerySingleOrDefault<Categorie>(sql, new { IdCategorie = categorieId });
    }

    public void AddCategorie(Categorie categorie)
    {
        const string sql = """
            INSERT INTO Categorie (NomCategorie, Descriptif)
            VALUES (@NomCategorie, @Descriptif);
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, categorie);
    }

    public bool UpdateCategorie(Categorie categorie)
    {
        const string sql = """
            UPDATE Categorie
            SET
                NomCategorie = @NomCategorie,
                Descriptif = @Descriptif
            WHERE IdCategorie = @IdCategorie;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesModifiees = connection.Execute(sql, categorie);
        return lignesModifiees > 0;
    }

    public bool DeleteCategorie(int categorieId)
    {
        const string sql = """
            DELETE FROM Categorie
            WHERE IdCategorie = @IdCategorie;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesModifiees = connection.Execute(sql, new { IdCategorie = categorieId });
        return lignesModifiees > 0;
    }

    public bool DeleteCategorieWithReaffectation(int idCategorieASupprimer, int idCategorieDestination)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            const string updateProduitsSql = """
                UPDATE Produit
                SET CategorieId = @IdCategorieDestination
                WHERE CategorieId = @IdCategorieASupprimer;
                """;

            connection.Execute(updateProduitsSql,
                new { IdCategorieASupprimer = idCategorieASupprimer, IdCategorieDestination = idCategorieDestination },
                transaction
            );

            const string deleteCategorieSql = """
                DELETE FROM Categorie
                WHERE IdCategorie = @IdCategorieASupprimer;
                """;

            int lignesSupprimees = connection.Execute(deleteCategorieSql,
                new { IdCategorieASupprimer = idCategorieASupprimer }, transaction
            );

            transaction.Commit();
            return lignesSupprimees > 0;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}