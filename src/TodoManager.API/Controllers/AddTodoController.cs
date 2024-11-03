using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Requests;
using TodoManager.Domain.Contracts.Response;

namespace TodoManager.API.Controllers
{
    public partial class TodoController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseTodoJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]
        public IActionResult Add([FromBody] RequestTodoJson request)
        {
            return _addTodoUseCase.Execute(request);
        }
    }
}
