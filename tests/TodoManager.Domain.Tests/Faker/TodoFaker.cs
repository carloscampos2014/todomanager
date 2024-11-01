using Bogus;
using TodoManager.Domain.Contracts.Enums;
using TodoManager.Domain.Contracts.Requests;

namespace TodoManager.Domain.Tests.Faker;

public static class TodoFaker
{
    public static IEnumerable<RequestTodoJson> GenerateList(int cont)
    {
        var faker = new Faker<RequestTodoJson>()
            .RuleFor(r => r.Name, f => f.Random.Words(3))
            .RuleFor(r => r.Description, f => f.Random.Words(7))
            .RuleFor(r => r.Deadline, f => f.Date.Recent())
            .RuleFor(r => r.Priority, f => f.PickRandom<PriorityType>())
            .RuleFor(r => r.Status, f => f.PickRandom<StatusType>());

        return faker.Generate(cont);
    }

    public static RequestTodoJson GenerateObject()
    {
        return GenerateList(1).FirstOrDefault() ?? new RequestTodoJson();
    }
}
