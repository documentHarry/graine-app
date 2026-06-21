using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Database;

public class MySqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public MySqlConnectionFactory(IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("MySqlConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("La chaîne de connexion MySQL est obligatoire.");
        }

        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}