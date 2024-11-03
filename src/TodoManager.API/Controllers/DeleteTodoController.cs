using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Response;

namespace TodoManager.API.Controllers;

public partial class TodoController
{
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        return _deleteTodoUseCase.Execute(id);
    }
}
