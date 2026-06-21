using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database;

public class SqlServerConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlServerConnectionFactory(IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("SqlServerConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("La chaîne de connexion SQL Server est obligatoire.");
        }

        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}