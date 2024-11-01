using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Interfaces.Repositories;
using TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;
using TodoManager.Domain.Contracts.Response;

namespace TodoManager.Domain.UseCases.Todo;

public class GetAllTodoUseCase : IGetAllTodoUseCase
{
    private readonly ITodoRepository _todoRepository;

    public GetAllTodoUseCase(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public IActionResult Execute()
    {
        try
        {
            var list = _todoRepository.GetAll();
            if (!list.Any())
            {
                return new NoContentResult();
            }

            var result = new ResponseAllTodoJson()
            {
                Todos = list.Select(todo => new ResponseTodoJson()
                {
                    Deadline = todo.Deadline,
                    Description = todo.Description,
                    Id = todo.Id,
                    Name = todo.Name,
                    Priority = todo.Priority,
                    Status = todo.Status,
                }).ToList(),
            };

            return new OkObjectResult(result);
        }
        catch (Exception)
        {
            var error = new ResponseErrorsJson()
            {
                Errors = ["Ocorreu um erro interno no servidor."],
            };

            return new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
