using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class EspeceRepository(IDbConnectionFactory connectionFactory) : IEspeceRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public IEnumerable<Espece> GetAllEspeces()
    {
        const string sql = """
            SELECT
                e.IdEspece,
                e.NomCommun,
                e.NomScientifique,
                COUNT(v.IdVariete) AS NombreVarietes
            FROM Espece e
            LEFT JOIN Variete v
                ON v.EspeceId = e.IdEspece
            GROUP BY
                e.IdEspece,
                e.NomCommun,
                e.NomScientifique
            ORDER BY e.NomCommun;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.Query<Espece>(sql);
    }

    public Espece? GetEspeceById(int especeId)
    {
        const string sql = """
            SELECT
                e.IdEspece,
                e.NomCommun,
                e.NomScientifique,
                COUNT(v.IdVariete) AS NombreVarietes
            FROM Espece e
            LEFT JOIN Variete v
                ON v.EspeceId = e.IdEspece
            WHERE e.IdEspece = @IdEspece
            GROUP BY
                e.IdEspece,
                e.NomCommun,
                e.NomScientifique;
            """;

        using var connection = _connectionFactory.CreateConnection();
        return connection.QuerySingleOrDefault<Espece>(sql, new { IdEspece = especeId });
    }

    public void AddEspece(Espece espece)
    {
        const string sql = """
            INSERT INTO Espece (NomCommun, NomScientifique)
            VALUES (@NomCommun, @NomScientifique);
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, espece);
    }

    public bool UpdateEspece(Espece espece)
    {
        const string sql = """
            UPDATE Espece
            SET
                NomCommun = @NomCommun,
                NomScientifique = @NomScientifique
            WHERE IdEspece = @IdEspece;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesModifiees = connection.Execute(sql, espece);
        return lignesModifiees > 0;
    }

    public bool DeleteEspece(int especeId)
    {
        const string sql = """
            DELETE FROM Espece
            WHERE IdEspece = @IdEspece;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesSupprimees = connection.Execute(sql, new { IdEspece = especeId } );
        return lignesSupprimees > 0;
    }
}