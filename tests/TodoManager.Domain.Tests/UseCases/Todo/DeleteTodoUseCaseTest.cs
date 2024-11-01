using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoManager.Domain.Contracts.Interfaces.Repositories;
using TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;
using TodoManager.Domain.Contracts.Requests;
using TodoManager.Domain.Tests.Faker;
using TodoManager.Domain.UseCases.Todo;
using TodoManager.Domain.Contracts.Dto;
using FluentAssertions;
using FluentAssertions.Equivalency;

namespace TodoManager.Domain.Tests.UseCases.Todo;

public class DeleteTodoUseCaseTest
{
    [Fact(DisplayName = "Não Deve Excluir Tarefa Quando Não Encontrar Registro")]
    public void Should_NotDeleteTodo_WhenRegistreNotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        TodoViewModel model = null;
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetById(id)).Returns(model);
        todoRespositoryMock.Setup(s => s.Delete(model)).Returns(true);
        IDeleteTodoUseCase useCase = new DeleteTodoUseCase(todoRespositoryMock.Object);

        // Act
        IActionResult actual = useCase.Execute(id);

        // Asserts
        actual.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status404NotFound);

        todoRespositoryMock.Verify(s => s.GetById(id), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.Delete(model), Times.Never);
    }

    [Fact(DisplayName = "Não Deve Excluir Tarefa Quando Repositório Falhar na Exclusão")]
    public void Should_NotDeleteTodo_WhenRepositoryFailDelete()
    {
        // Arrange
        TodoViewModel model = TodoFaker.GenerateTodoObject();
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetById(model.Id)).Returns(model);
        todoRespositoryMock.Setup(s => s.Delete(model)).Returns(false);
        IDeleteTodoUseCase useCase = new DeleteTodoUseCase(todoRespositoryMock.Object);

        // Act
        IActionResult actual = useCase.Execute(model.Id);

        // Asserts
        actual.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

        todoRespositoryMock.Verify(s => s.GetById(model.Id), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.Delete(model), Times.Exactly(1));
    }

    [Fact(DisplayName = "Deve Excluir Tarefa Quando Repositório Excluir com Sucesso")]
    public void Should_DeleteTodo_WhenRepositoryExcludeSucess()
    {
        // Arrange
        TodoViewModel model = TodoFaker.GenerateTodoObject();
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetById(model.Id)).Returns(model);
        todoRespositoryMock.Setup(s => s.Delete(model)).Returns(true);
        IDeleteTodoUseCase useCase = new DeleteTodoUseCase(todoRespositoryMock.Object);

        // Act
        IActionResult actual = useCase.Execute(model.Id);

        // Asserts
        actual.Should().BeOfType<NoContentResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        todoRespositoryMock.Verify(s => s.GetById(model.Id), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.Delete(model), Times.Exactly(1));
    }
}
