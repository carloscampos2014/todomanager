using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Interfaces.Repositories;
using TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;
using TodoManager.Domain.Contracts.Response;

namespace TodoManager.Domain.UseCases.Todo;

public class DeleteTodoUseCase : IDeleteTodoUseCase
{
    private readonly ITodoRepository _todoRepository;

    public DeleteTodoUseCase(ITodoRepository todoRepository)
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

                return new ObjectResult(error)
                {
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var resultrepository = _todoRepository.Delete(model);

            if (!resultrepository)
            {
                throw new InvalidOperationException("Erro na inclusão dos dados.");
            }

            return new NoContentResult();
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
