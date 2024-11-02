using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoManager.Domain.Contracts.Dto;
using TodoManager.Domain.Contracts.Interfaces.Repositories;
using TodoManager.Domain.Tests.Faker;
using TodoManager.Domain.UseCases.Todo;

namespace TodoManager.Domain.Tests.UseCases.Todo;

public class GetByIdTodoUseCaseTest
{
    [Fact(DisplayName = "Deve Retornar um Erro 500 Quando Ocorrer Erro no Repositório.")]
    public void Should_ReturnInternalServerError_WhenOccursExceptionRepository()
    {
        // Arrange
        var model = TodoFaker.GenerateTodoObject();
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetById(model.Id)).Throws(new Exception());

        // Act
        var actual = new GetByIdTodoUseCase(todoRespositoryMock.Object).Execute(model.Id);

        // Asserts
        actual.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

        todoRespositoryMock.Verify(s => s.GetById(model.Id), Times.Exactly(1));
    }

    [Fact(DisplayName = "Deve Retornar NotFound Quando Repositório não Encontrar Registro.")]
    public void Should_ReturnNotFound_WhenRepositoryNotFoundRegister()
    {
        // Arrange
        var id = Guid.NewGuid();
        TodoViewModel model = null;
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetById(id)).Returns(model);

        // Act
        var actual = new GetByIdTodoUseCase(todoRespositoryMock.Object).Execute(id);

        // Asserts
        actual.Should().BeOfType<NotFoundObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status404NotFound);

        todoRespositoryMock.Verify(s => s.GetById(id), Times.Exactly(1));
    }

    [Fact(DisplayName = "Deve Retornar 200Ok Quando Repositório Encontrar Registro.")]
    public void Should_Return200OK_WhenRepositoryFoundRegister()
    {
        // Arrange
        var model = TodoFaker.GenerateTodoObject();
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetById(model.Id)).Returns(model);

        // Act
        var actual = new GetByIdTodoUseCase(todoRespositoryMock.Object).Execute(model.Id);

        // Asserts
        actual.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status200OK);

        todoRespositoryMock.Verify(s => s.GetById(model.Id), Times.Exactly(1));
    }
}
