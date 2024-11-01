using TodoManager.Domain.Contracts.Requests;

namespace TodoManager.Domain.Contracts.Dto;

public class TodoViewModel : RequestTodoJson
{
    public Guid Id { get; set; } = Guid.NewGuid();
}
