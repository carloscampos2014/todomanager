namespace TodoManager.Domain.Contracts.Response;

public class ResponseAllTodoJson
{
    public IEnumerable<ResponseTodoJson> Todos { get; set; } = [];
}
