using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class ProduitRepository(IDbConnectionFactory connectionFactory) : IProduitRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public IEnumerable<Produit> GetAllProduits()
    {
        const string sql = """
            SELECT
                p.*,

                c.IdCategorie,
                c.NomCategorie,
                c.Descriptif,

                v.*,

                e.IdEspece,
                e.NomCommun,
                e.NomScientifique

            FROM Produit p

            INNER JOIN Categorie c
                ON c.IdCategorie = p.CategorieId

            INNER JOIN Variete v
                ON v.IdVariete = p.VarieteId

            INNER JOIN Espece e
                ON e.IdEspece = v.EspeceId

            ORDER BY p.Intitule;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return connection.Query<Produit, Categorie, Variete, Espece, Produit>(
            sql,
            (produit, categorie, variete, espece) =>
            {
                variete.Espece = espece;
                produit.Categorie = categorie;
                produit.Variete = variete;
                return produit;
            },
            splitOn: "IdCategorie,IdVariete,IdEspece"
        );
    }

    public Produit? GetProduitById(int produitId)
    {
        return GetAllProduits().FirstOrDefault(p => p.IdProduit == produitId);
    }

    public IEnumerable<Produit> GetProduitsByCategorie(int categorieId)
    {
        return GetAllProduits().Where(p => p.CategorieId == categorieId);
    }

    public void AddProduit(Produit produit)
    {
        const string sql = """
            INSERT INTO Produit (
                Intitule,
                PrixUnitaire,
                Quantite,
                ImageProduit,
                DateAjout,
                CategorieId,
                VarieteId
            )
            VALUES (
                @Intitule,
                @PrixUnitaire,
                @Quantite,
                @ImageProduit,
                @DateAjout,
                @CategorieId,
                @VarieteId
            );
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, produit);
    }

    public bool UpdateProduit(Produit produit)
    {
        const string sql = """
            UPDATE Produit
            SET
                Intitule = @Intitule,
                PrixUnitaire = @PrixUnitaire,
                Quantite = @Quantite,
                CategorieId = @CategorieId,
                VarieteId = @VarieteId
            WHERE IdProduit = @IdProduit;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesModifiees = connection.Execute(sql, produit);
        return lignesModifiees > 0;
    }

    public bool DeleteProduit(int produitId)
    {
        const string sql = """
            DELETE FROM Produit
            WHERE IdProduit = @IdProduit;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesSupprimees = connection.Execute(sql, new { IdProduit = produitId });
        return lignesSupprimees > 0;
    }
}