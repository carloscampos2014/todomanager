using Bogus;
using TodoManager.Domain.Contracts.Dto;
using TodoManager.Domain.Contracts.Enums;
using TodoManager.Domain.Contracts.Requests;

namespace TodoManager.Domain.Tests.Faker;

public static class TodoFaker
{
    public static IEnumerable<RequestTodoJson> GenerateRequestList(int cont)
    {
        var faker = new Faker<RequestTodoJson>()
            .RuleFor(r => r.Name, f => f.Random.Words(3))
            .RuleFor(r => r.Description, f => f.Random.Words(7))
            .RuleFor(r => r.Deadline, f => f.Date.Recent())
            .RuleFor(r => r.Priority, f => f.PickRandom<PriorityType>())
            .RuleFor(r => r.Status, f => f.PickRandom<StatusType>());

        return faker.Generate(cont);
    }

    public static RequestTodoJson GenerateRequestObject()
    {
        return GenerateRequestList(1).FirstOrDefault() ?? new RequestTodoJson();
    }
    
    public static IEnumerable<TodoViewModel> GenerateTodoList(int cont)
    {
        var faker = new Faker<TodoViewModel>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Name, f => f.Random.Words(3))
            .RuleFor(r => r.Description, f => f.Random.Words(7))
            .RuleFor(r => r.Deadline, f => f.Date.Recent())
            .RuleFor(r => r.Priority, f => f.PickRandom<PriorityType>())
            .RuleFor(r => r.Status, f => f.PickRandom<StatusType>());

        return faker.Generate(cont);
    }

    public static TodoViewModel GenerateTodoObject()
    {
        return GenerateTodoList(1).FirstOrDefault() ?? new TodoViewModel();
    }
}
