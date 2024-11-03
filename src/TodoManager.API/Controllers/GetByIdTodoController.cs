using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Response;

namespace TodoManager.API.Controllers;

public partial class TodoController
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseTodoJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        return _getByIdTodoUseCase.Execute(id);
    }
}
