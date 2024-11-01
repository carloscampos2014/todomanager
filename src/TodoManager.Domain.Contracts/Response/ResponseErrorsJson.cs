namespace TodoManager.Domain.Contracts.Response;

public class ResponseErrorsJson
{
    public IEnumerable<string> Errors { get; set; } = [];
}
