using Dapper;
using TodoManager.Database.Sqlite.Factories;
using TodoManager.Domain.Contracts.Dto;

namespace TodoManager.Database.Sqlite.Tests.Fixtures;

public class DataBaseFixture : IDisposable
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly string _databaseName;

    public DataBaseFixture()
    {
        string filename = Path.Combine(Directory.GetCurrentDirectory(), ".env");
        _databaseName = Path.Combine(Path.GetTempPath(), $"TestDb_{Guid.NewGuid()}.db");
        File.WriteAllText( filename, $"DB_NAME ={_databaseName}" );
        _connectionFactory = new ConnectionFactory();
    }

    public IConnectionFactory ConnectionFactory => _connectionFactory;

    public void AddTodo(TodoViewModel model)
    {
        using var connection = _connectionFactory.CreateConnection();
        string sql = @"
            INSERT INTO Todos(Id, Name, Description, DeadLine, Priority, Status)
            VALUES(@id, @name, @description, @deadline, @priority, @status);";

        connection.Execute(sql, new {
            id = model.Id.ToString(),
            name = model.Name,
            description = model.Description,
            deadline = model.Deadline,
            priority = model.Priority,
            status = model.Status
        });
    }

    public void Clear()
    {
        using var connection = _connectionFactory.CreateConnection();
        string sql = @"DELETE FROM Todos;";

        connection.Execute(sql);
    }

    public void Dispose()
    {
        if (File.Exists(_databaseName))
        {
            File.Delete(_databaseName);
        }
    }
}

[CollectionDefinition("Database")]
public class DatabaseCollection : ICollectionFixture<DataBaseFixture>
{
    // Essa classe não contém código, serve apenas como ponto de definição.
}
