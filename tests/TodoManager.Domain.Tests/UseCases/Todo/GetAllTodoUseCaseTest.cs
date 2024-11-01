using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoManager.Domain.Contracts.Dto;
using TodoManager.Domain.Contracts.Interfaces.Repositories;
using TodoManager.Domain.Contracts.Interfaces.UseCases.Todo;
using TodoManager.Domain.Tests.Faker;
using TodoManager.Domain.UseCases.Todo;

namespace TodoManager.Domain.Tests.UseCases.Todo;

public class GetAllTodoUseCaseTest
{
    [Fact(DisplayName = "Deve Retornar um Erro 500 Quando Ocorrer Erro no Repositório.")]
    public void Should_ReturnInternalServerError_WhenOccursExceptionRepository()
    {
        // Arrange
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetAll()).Throws(new Exception());
        IGetAllTodoUseCase useCase = new GetAllTodoUseCase(todoRespositoryMock.Object);

        // Act
        var actual = useCase.Execute();

        // Asserts
        actual.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

        todoRespositoryMock.Verify(s => s.GetAll(), Times.Exactly(1));
    }

    [Fact(DisplayName = "Deve Retornar NoContent Quando Repositório Retornar Lista Vazia.")]
    public void Should_ReturnNoContent_WhenRepositoryReturnsEmptyList()
    {
        // Arrange
        var list = new List<TodoViewModel>();
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetAll()).Returns(list);
        IGetAllTodoUseCase useCase = new GetAllTodoUseCase(todoRespositoryMock.Object);

        // Act
        var actual = useCase.Execute();

        // Asserts
        actual.Should().BeOfType<NoContentResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        todoRespositoryMock.Verify(s => s.GetAll(), Times.Exactly(1));
    }

    [Fact(DisplayName = "Deve Retornar 200Ok Quando Repositório Retornar Lista Preenchida.")]
    public void Should_ReturnNoContent_WhenRepositoryReturnsCompleteList()
    {
        // Arrange
        var list = TodoFaker.GenerateTodoList(10);
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetAll()).Returns(list);
        IGetAllTodoUseCase useCase = new GetAllTodoUseCase(todoRespositoryMock.Object);

        // Act
        var actual = useCase.Execute();

        // Asserts
        actual.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status200OK);

        todoRespositoryMock.Verify(s => s.GetAll(), Times.Exactly(1));
    }
}
