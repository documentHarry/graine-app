using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class AdresseLivraisonRepository(IDbConnectionFactory connectionFactory) : IAdresseLivraisonRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public IEnumerable<AdresseLivraison> GetAdressesByUtilisateur(int utilisateurId)
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

    public AdresseLivraison? GetAdresseById(int adresseId)
    {
        return GetToutesLesAdresses().FirstOrDefault(a => a.IdAdresse == adresseId);
    }

    public void AddAdresse(AdresseLivraison adresse)
    {
        const string sql = """
            INSERT INTO AdresseLivraison (
                Rue,
                Numero,
                ParDefaut,
                UtilisateurId,
                LocaliteId
            )
            VALUES (
                @Rue,
                @Numero,
                @ParDefaut,
                @UtilisateurId,
                @LocaliteId
            );
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, adresse);
    }

    public bool UpdateAdresse(AdresseLivraison adresse)
    {
        const string sql = """
            UPDATE AdresseLivraison
            SET
                Rue = @Rue,
                Numero = @Numero,
                ParDefaut = @ParDefaut,
                LocaliteId = @LocaliteId
            WHERE IdAdresse = @IdAdresse;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesModifiees = connection.Execute(sql, adresse);
        return lignesModifiees > 0;
    }

    public bool DeleteAdresse(int adresseId)
    {
        const string sql = """
            DELETE FROM AdresseLivraison
            WHERE IdAdresse = @IdAdresse;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesSupprimees = connection.Execute(sql, new { IdAdresse = adresseId });
        return lignesSupprimees > 0;
    }

    private IEnumerable<AdresseLivraison> GetToutesLesAdresses()
    {
        const string sql = """
            SELECT
                a.*,

                l.IdLocalite,
                l.CodePostal,
                l.NomLocalite

            FROM AdresseLivraison a

            INNER JOIN Localite l
                ON l.IdLocalite = a.LocaliteId;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return connection.Query<AdresseLivraison, Localite, AdresseLivraison>(
            sql, (adresse, localite) =>
            {
                adresse.Localite = localite;
                return adresse;
            },
            splitOn: "IdLocalite"
        );
    }
}