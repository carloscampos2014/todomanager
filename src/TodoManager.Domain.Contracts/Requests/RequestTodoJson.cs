using TodoManager.Domain.Contracts.Enums;

namespace TodoManager.Domain.Contracts.Requests;

public class RequestTodoJson
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime Deadline { get; set; } = DateTime.UtcNow;

    public PriorityType Priority { get; set; } = PriorityType.Lower;

    public StatusType Status { get; set; } = StatusType.Created;
}
