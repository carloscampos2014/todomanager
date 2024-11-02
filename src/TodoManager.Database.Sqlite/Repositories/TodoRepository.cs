using Dapper;
using TodoManager.Database.Sqlite.Factories;
using TodoManager.Domain.Contracts.Dto;
using TodoManager.Domain.Contracts.Enums;
using TodoManager.Domain.Contracts.Interfaces.Repositories;

namespace TodoManager.Database.Sqlite.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public TodoRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public bool Add(TodoViewModel model)
    {
        if (model is null)
        { 
            return false;
        }
        using var connection = _connectionFactory.CreateConnection();
        string sql = @"
            INSERT INTO Todos(Id, Name, Description, DeadLine, Priority, Status)
            VALUES(@id, @name, @description, @deadline, @priority, @status);";

        return connection.Execute(sql, new {
            id = model.Id.ToString(),
            name = model.Name,
            description = model.Description,
            deadline = model.Deadline,
            priority = model.Priority,
            status = model.Status
        }) > 0;
    }

    public bool Delete(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();
        string sql = @"
            DELETE FROM Todos WHERE Id = @Id;";

        return connection.Execute(sql, new { Id = id.ToString() }) > 0;
    }

    public IEnumerable<TodoViewModel> GetAll()
    {
        using var connection = _connectionFactory.CreateConnection();
        string sql = @"
            SELECT Id, Name, Description, DeadLine, Priority, Status FROM Todos ORDER BY Status, Name;";

        var result = connection.Query(sql)
            .Select(row => new TodoViewModel
            {
                Id = Guid.Parse((string)row.Id), // Converte de string para Guid
                Name = row.Name,
                Description = row.Description,
                Deadline = row.Deadline,
                Priority = (PriorityType)row.Priority,
                Status = (StatusType)row.Status
            }).ToList() ;

        return result;
    }

    public TodoViewModel? GetById(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();
        string sql = @"
        SELECT Id, Name, Description, Deadline, Priority, Status FROM Todos WHERE Id = @Id;";

        var result = connection.Query(sql, new { Id = id.ToString() })
            .Select(row => new TodoViewModel
            {
                Id = Guid.Parse((string)row.Id), // Converte de string para Guid
                Name = row.Name,
                Description = row.Description,
                Deadline = row.Deadline,
                Priority = (PriorityType)row.Priority,
                Status = (StatusType)row.Status
            })
            .FirstOrDefault();

        return result;
    }

    public bool Update(TodoViewModel model)
    {
        if (model is null)
        {
            return false;
        }

        using var connection = _connectionFactory.CreateConnection();
        string sql = @"
            UPDATE Todos SET
                Name = @name,
                Description = @description,
                Deadline = @deadline,
                Priority = @priority,
                Status = @status
            WHERE Id = @id;";

        return connection.Execute(sql, new {
            id = model.Id.ToString(),
            name = model.Name,
            description = model.Description,
            deadline = model.Deadline,
            priority = model.Priority,
            status = model.Status
        }) > 0;
    }
}
