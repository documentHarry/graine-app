using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class AromateRepository(IDbConnectionFactory connectionFactory) : IAromateRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public IEnumerable<Aromate> GetAllAromates()
    {
        const string sql = """
            SELECT
                a.IdAromate, a.VarieteId, a.PartieUtilisee, a.Propriete, a.UsageCulinaire,
                v.IdVariete, v.Nom, v.EspeceId,
                e.IdEspece, e.NomScientifique, e.NomCommun,
                ap.AromateId, ap.ProprieteId,
                pm.IdPropriete, pm.NomPropriete
            FROM Aromate a
            INNER JOIN Variete v ON v.IdVariete = a.VarieteId
            INNER JOIN Espece e ON e.IdEspece = v.EspeceId
            LEFT JOIN AromatePropriete ap ON ap.AromateId = a.IdAromate
            LEFT JOIN ProprieteMedicinale pm ON pm.IdPropriete = ap.ProprieteId
            ORDER BY v.Nom;
            """;

        using var connection = _connectionFactory.CreateConnection();

        var aromates = new Dictionary<int, Aromate>();

        connection.Query<Aromate, Variete, Espece, AromatePropriete, ProprieteMedicinale, Aromate>(
            sql, (aromate, variete, espece, aromatePropriete, proprieteMedicinale) =>
            {
                if (!aromates.TryGetValue(aromate.IdAromate, out var aromateExistant))
                {
                    variete.Espece = espece;
                    aromate.Variete = variete;
                    aromate.AromateProprietes = [];
                    aromates.Add(aromate.IdAromate, aromate);
                    aromateExistant = aromate;
                }

                if (aromatePropriete != null && aromatePropriete.ProprieteId != 0)
                {
                    aromatePropriete.ProprieteMedicinale = proprieteMedicinale;
                    aromateExistant.AromateProprietes.Add(aromatePropriete);
                }
                return aromateExistant;
            },
            splitOn: "IdVariete,IdEspece,AromateId,IdPropriete"
        );

        return aromates.Values;
    }

    public Aromate? GetAromateById(int aromateId)
    {
        return GetAllAromates().FirstOrDefault(aromate => aromate.IdAromate == aromateId);
    }

    public void AddAromate(Aromate aromate)
    {
        const string sql = """
            INSERT INTO Aromate ( PartieUtilisee, Propriete, UsageCulinaire, VarieteId )
            OUTPUT INSERTED.IdAromate
            VALUES ( @PartieUtilisee, @Propriete, @UsageCulinaire, @VarieteId );
            """;

        const string sqlPropriete = """
            INSERT INTO AromatePropriete ( AromateId, ProprieteId )
            VALUES ( @AromateId, @ProprieteId );
            """;

        using var connection = _connectionFactory.CreateConnection();
        var idAromate = connection.QuerySingle<int>(sql, aromate);

        foreach (var aromatePropriete in aromate.AromateProprietes)
        {
            connection.Execute(sqlPropriete, new
            {
                AromateId = idAromate,
                aromatePropriete.ProprieteId
            });
        }
    }

    public bool UpdateAromate(Aromate aromate)
    {
        const string sql = """
            UPDATE Aromate
            SET
                PartieUtilisee = @PartieUtilisee,
                Propriete = @Propriete,
                UsageCulinaire = @UsageCulinaire,
                VarieteId = @VarieteId
            WHERE IdAromate = @IdAromate;
            """;

        const string sqlDeleteProprietes = """
            DELETE FROM AromatePropriete
            WHERE AromateId = @AromateId;
            """;

        const string sqlInsertPropriete = """
            INSERT INTO AromatePropriete (AromateId, ProprieteId)
            VALUES (@AromateId, @ProprieteId);
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesModifiees = connection.Execute(sql, aromate);
        connection.Execute(sqlDeleteProprietes, new { AromateId = aromate.IdAromate });

        foreach (var aromatePropriete in aromate.AromateProprietes)
        {
            connection.Execute(sqlInsertPropriete, new { AromateId = aromate.IdAromate, aromatePropriete.ProprieteId});
        }
        return lignesModifiees > 0;
    }

    public bool DeleteAromate(int aromateId)
    {
        const string sqlDeleteProprietes = """
            DELETE FROM AromatePropriete
            WHERE AromateId = @AromateId;
            """;

        const string sqlDeleteAromate = """
            DELETE FROM Aromate
            WHERE IdAromate = @IdAromate;
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sqlDeleteProprietes, new { AromateId = aromateId });
        int lignesSupprimees = connection.Execute(sqlDeleteAromate, new { IdAromate = aromateId });
        return lignesSupprimees > 0;
    }
}