﻿using Microsoft.AspNetCore.Mvc;
using TodoManager.Domain.Contracts.Requests;

namespace TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;

public interface IAddTodoUseCase
{
    IActionResult Execute(RequestTodoJson request);
}