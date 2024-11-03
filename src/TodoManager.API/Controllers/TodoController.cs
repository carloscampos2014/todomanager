using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;

namespace TodoManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class TodoController : ControllerBase
    {
        private readonly IAddTodoUseCase _addTodoUseCase;
        private readonly IDeleteTodoUseCase _deleteTodoUseCase;
        private readonly IGetAllTodoUseCase _getAllTodoUseCase;
        private readonly IGetByIdTodoUseCase _getByIdTodoUseCase;
        private readonly IUpdateTodoUseCase _updateTodoUseCase;

        public TodoController(IAddTodoUseCase addTodoUseCase, IDeleteTodoUseCase deleteTodoUseCase, IGetAllTodoUseCase getAllTodoUseCase, IGetByIdTodoUseCase getByIdTodoUseCase, IUpdateTodoUseCase updateTodoUseCase)
        {
            _addTodoUseCase = addTodoUseCase;
            _deleteTodoUseCase = deleteTodoUseCase;
            _getAllTodoUseCase = getAllTodoUseCase;
            _getByIdTodoUseCase = getByIdTodoUseCase;
            _updateTodoUseCase = updateTodoUseCase;
        }
    }
}
