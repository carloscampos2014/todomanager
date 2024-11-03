using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Interfaces.Repositories;
using TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;
using TodoManager.Domain.Contracts.Requests;
using TodoManager.Domain.Contracts.Response;

namespace TodoManager.Domain.UseCases.Todo;


public class UpdateTodoUseCase : IUpdateTodoUseCase
{
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<RequestTodoJson> _validator;

    public UpdateTodoUseCase(
        ITodoRepository todoRepository,
        IValidator<RequestTodoJson> validator)
    {
        _todoRepository = todoRepository;
        _validator = validator;
    }

    public IActionResult Execute(Guid id, RequestTodoJson request)
    {
        try
        {
            var resultValidator = _validator.Validate(request);
            if (!resultValidator.IsValid)
            {
                var error = new ResponseErrorsJson()
                {
                    Errors = resultValidator.Errors.Select(error => error.ErrorMessage).ToList(),
                };

                return new BadRequestObjectResult(error);
            }

            var model = _todoRepository.GetById(id);

            if (model is null)
            {
                var error = new ResponseErrorsJson()
                {
                    Errors = [$"Não foi encontrado registro para Id:{id}."],
                };

                return new NotFoundObjectResult(error);
            }

            model.Name = request.Name;
            model.Description = request.Description;
            model.Priority = request.Priority;
            model.Status = request.Status;
            model.Deadline = request.Deadline;
            var resultrepository = _todoRepository.Update(model);

            if (!resultrepository)
            {
                throw new InvalidOperationException("Erro na alteração dos dados.");
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