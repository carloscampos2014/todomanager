using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TodoManager.Database.Sqlite.Factories;
using TodoManager.Database.Sqlite.Repositories;
using TodoManager.Domain.Contracts.Interfaces.Repositories;
using TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;
using TodoManager.Domain.Contracts.Requests;
using TodoManager.Domain.UseCases.Todo;
using TodoManager.Domain.Validators;

namespace TodoManager.Ioc;
public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IValidator<RequestTodoJson>, RequestTodoValidator>();
        services.AddScoped<IAddTodoUseCase, AddTodoUseCase>();
        services.AddScoped<IDeleteTodoUseCase, DeleteTodoUseCase>();
        services.AddScoped<IGetAllTodoUseCase, GetAllTodoUseCase>();
        services.AddScoped<IGetByIdTodoUseCase, GetByIdTodoUseCase>();
        services.AddScoped<IUpdateTodoUseCase, UpdateTodoUseCase>();
    }
}
