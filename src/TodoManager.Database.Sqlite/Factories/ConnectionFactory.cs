using System.Data;
using System.Data.SQLite;
using Dapper;
using DotNetEnv;

namespace TodoManager.Database.Sqlite.Factories;

public class ConnectionFactory : IConnectionFactory
{
    private readonly string _connectionString;

    public ConnectionFactory()
    {
        Env.Load();
        _connectionString = $"Data Source={Env.GetString("DB_NAME")}; Version=3;";
    }

    public IDbConnection CreateConnection()
    {
        var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        EnsureTableExists(connection);
        return connection;
    }

    private static void EnsureTableExists(IDbConnection connection)
    {
        var tableExists = connection.ExecuteScalar<int>(
            "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Todos';") > 0;

        if (tableExists)
        {
            return;
        }

        connection.Execute(@"
                CREATE TABLE Todos (
                    Id TEXT PRIMARY KEY NOT NULL,
                    Name TEXT NOT NULL,
                    Description TEXT NOT NULL,
                    Deadline DATETIME NOT NULL,
                    Priority INTEGER NOT NULL,
                    Status INTEGER NOT NULL
                );
            ");
    }
}
