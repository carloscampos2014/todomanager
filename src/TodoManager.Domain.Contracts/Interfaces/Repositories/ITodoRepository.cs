﻿using TodoManager.Domain.Contracts.Dto;

namespace TodoManager.Domain.Contracts.Interfaces.Repositories;

public interface ITodoRepository
{
    bool Add(TodoViewModel model);

    bool Delete(Guid id);

    TodoViewModel? GetById(Guid id);

    IEnumerable<TodoViewModel> GetAll();

    bool Update(TodoViewModel model);
}
