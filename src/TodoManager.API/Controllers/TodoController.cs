using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Requests;

namespace TodoManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public IActionResult Add([FromBody] RequestTodoJson request)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
        }
    }
}
