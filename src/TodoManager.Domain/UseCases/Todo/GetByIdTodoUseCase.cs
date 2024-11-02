using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Interfaces.Repositories;
using TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;
using TodoManager.Domain.Contracts.Response;

namespace TodoManager.Domain.UseCases.Todo;

public class GetByIdTodoUseCase : IGetByIdTodoUseCase
{
    private readonly ITodoRepository _todoRepository;

    public GetByIdTodoUseCase(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public IActionResult Execute(Guid id)
    {
        try
        {
            var model = _todoRepository.GetById(id);

            if (model is null)
            {
                var error = new ResponseErrorsJson()
                {
                    Errors = [$"Não foi encontrado registro para Id:{id}."],
                };

                return new NotFoundObjectResult(error);
            }

            return new OkObjectResult(new ResponseTodoJson()
            {
                Deadline = model.Deadline,
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                Priority = model.Priority,
                Status = model.Status,
            });
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
