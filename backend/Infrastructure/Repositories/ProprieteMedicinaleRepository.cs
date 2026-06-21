using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class ProprieteMedicinaleRepository(IDbConnectionFactory connectionFactory) : IProprieteMedicinaleRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public IEnumerable<ProprieteMedicinale> GetAllProprietesMedicinales()
    {
        const string sql = """
            SELECT
                IdPropriete,
                NomPropriete
            FROM ProprieteMedicinale
            ORDER BY NomPropriete;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.Query<ProprieteMedicinale>(sql);
    }

    public ProprieteMedicinale? GetProprieteMedicinaleById(int proprieteId)
    {
        const string sql = """
            SELECT
                IdPropriete,
                NomPropriete
            FROM ProprieteMedicinale
            WHERE IdPropriete = @IdPropriete;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.QuerySingleOrDefault<ProprieteMedicinale>( sql, new { IdPropriete = proprieteId }
        );
    }

    public void AddProprieteMedicinale(ProprieteMedicinale proprieteMedicinale)
    {
        const string sql = """
            INSERT INTO ProprieteMedicinale (NomPropriete)
            VALUES (@NomPropriete);
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, proprieteMedicinale);
    }

    public bool UpdateProprieteMedicinale(ProprieteMedicinale proprieteMedicinale)
    {
        const string sql = """
            UPDATE ProprieteMedicinale
            SET NomPropriete = @NomPropriete
            WHERE IdPropriete = @IdPropriete;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesModifiees = connection.Execute(sql, proprieteMedicinale);
        return lignesModifiees > 0;
    }

    public bool DeleteProprieteMedicinale(int proprieteId)
    {
        const string sql = """
            DELETE FROM ProprieteMedicinale
            WHERE IdPropriete = @IdPropriete;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesSupprimees = connection.Execute(sql, new { IdPropriete = proprieteId });
        return lignesSupprimees > 0;
    }
}