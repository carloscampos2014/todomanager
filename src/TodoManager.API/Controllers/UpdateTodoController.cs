using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Requests;
using TodoManager.Domain.Contracts.Response;

namespace TodoManager.API.Controllers;

public partial class TodoController
{
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResponseTodoJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]
    public IActionResult Update([FromRoute] Guid id, [FromBody] RequestTodoJson request)
    {
        return _updateTodoUseCase.Execute(id, request);
    }
}
