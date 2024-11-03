using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Response;

namespace TodoManager.API.Controllers;

public partial class TodoController
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllTodoJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        return _getAllTodoUseCase.Execute();
    }
}
