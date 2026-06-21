using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class LocaliteRepository(IDbConnectionFactory connectionFactory) : ILocaliteRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public IEnumerable<Localite> GetAllLocalites()
    {
        const string sql = """
            SELECT
                IdLocalite,
                CodePostal,
                NomLocalite
            FROM Localite
            ORDER BY CodePostal, NomLocalite;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.Query<Localite>(sql);
    }

    public Localite? GetLocaliteById(int localiteId)
    {
        const string sql = """
            SELECT
                IdLocalite,
                CodePostal,
                NomLocalite
            FROM Localite
            WHERE IdLocalite = @IdLocalite;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.QuerySingleOrDefault<Localite>(sql, new { IdLocalite = localiteId });
    }

    public void AddLocalite(Localite localite)
    {
        const string sql = """
            INSERT INTO Localite ( CodePostal, NomLocalite )
            VALUES ( @CodePostal, @NomLocalite );
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, localite);
    }

    public bool UpdateLocalite(Localite localite)
    {
        const string sql = """
            UPDATE Localite
            SET
                CodePostal = @CodePostal,
                NomLocalite = @NomLocalite
            WHERE IdLocalite = @IdLocalite;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesModifiees = connection.Execute(sql, localite);
        return lignesModifiees > 0;
    }

    public bool DeleteLocalite(int localiteId)
    {
        const string sql = """
            DELETE FROM Localite
            WHERE IdLocalite = @IdLocalite;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesSupprimees = connection.Execute(sql, new { IdLocalite = localiteId });
        return lignesSupprimees > 0;
    }
}