using Microsoft.AspNetCore.Mvc;

namespace TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;

public interface IDeleteTodoUseCase
{
    IActionResult Execute(Guid id);
}
