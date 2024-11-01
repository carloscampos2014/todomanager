using Microsoft.AspNetCore.Mvc;

namespace TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;

public interface IGetByIdTodoUseCase
{
    IActionResult Execute(Guid id);
}
