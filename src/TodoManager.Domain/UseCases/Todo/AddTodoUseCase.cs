using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Dto;
using TodoManager.Domain.Contracts.Interfaces.Repositories;
using TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;
using TodoManager.Domain.Contracts.Requests;
using TodoManager.Domain.Contracts.Response;

namespace TodoManager.Domain.UseCases.Todo;

public class AddTodoUseCase : IAddTodoUseCase
{
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<RequestTodoJson> _validator;

    public AddTodoUseCase(
        ITodoRepository todoRepository,
        IValidator<RequestTodoJson> validator)
    {
        _todoRepository = todoRepository;
        _validator = validator;
    }

    public IActionResult Execute(RequestTodoJson request)
    {
        try
        {
            var resultValidator = _validator.Validate(request);
            if (!resultValidator.IsValid)
            {
                var error = new ResponseErrorsJson()
                {
                    Errors = resultValidator.Errors.Select( error => error.ErrorMessage).ToList(),
                };

                return new BadRequestObjectResult(error);
            }

            var model = new TodoViewModel() 
            { 
                Name = request.Name,
                Deadline = request.Deadline,
                Description = request.Description,
                Priority = request.Priority,
                Status = request.Status,
            };

            var resultrepository = _todoRepository.Add(model);

            if (!resultrepository)
            {
                throw new InvalidOperationException("Erro na inclusão dos dados.");
            }

            return new ObjectResult(new ResponseTodoJson()
            {
                Deadline = model.Deadline,
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                Priority = model.Priority,
                Status = model.Status,
            })
            {
                StatusCode = StatusCodes.Status201Created,
            };
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
