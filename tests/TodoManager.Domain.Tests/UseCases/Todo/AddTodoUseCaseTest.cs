using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoManager.Domain.Contracts.Dto;
using TodoManager.Domain.Contracts.Interfaces.Repositories;
using TodoManager.Domain.Contracts.Requests;
using TodoManager.Domain.Tests.Faker;
using TodoManager.Domain.UseCases.Todo;

namespace TodoManager.Domain.Tests.UseCases.Todo;

public class AddTodoUseCaseTest
{
    [Fact(DisplayName = "Não Deve Adicionar Tarefa Quando a Validação Falhar.")]
    public void Should_NotAddTodo_WhenValidationFail()
    {
        // Arrange
        var request = TodoFaker.GenerateRequestObject();
        var validatorMock = new Mock<IValidator<RequestTodoJson>>();
        var resultValidator = new ValidationResult(new List<ValidationFailure>() { new ValidationFailure(nameof(RequestTodoJson), "O objeto da lista não pode ser nulo.") });
        validatorMock.Setup(s => s.Validate(request)).Returns(resultValidator);
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.Add(It.IsAny<TodoViewModel>())).Returns(true);

        // Act
        var actual = new AddTodoUseCase(todoRespositoryMock.Object, validatorMock.Object).Execute(request);

        // Asserts
        actual.Should().BeOfType<BadRequestObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        validatorMock.Verify(s => s.Validate(request), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.Add(It.IsAny<TodoViewModel>()), Times.Never);
    }

    [Fact(DisplayName = "Não Deve Adicionar Tarefa Quando Repositório Falhar na Inclusão.")]
    public void Should_NotAddTodo_WhenRepositoryFailInclude()
    {
        // Arrange
        var request = TodoFaker.GenerateRequestObject();
        var validatorMock = new Mock<IValidator<RequestTodoJson>>();
        var resultValidator = new ValidationResult();
        validatorMock.Setup(s => s.Validate(request)).Returns(resultValidator);
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.Add(It.IsAny<TodoViewModel>())).Returns(false);

        // Act
        var actual = new AddTodoUseCase(todoRespositoryMock.Object, validatorMock.Object).Execute(request);

        // Asserts
        actual.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

        validatorMock.Verify(s => s.Validate(request), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.Add(It.IsAny<TodoViewModel>()), Times.Exactly(1));
    }


    [Fact(DisplayName = "Deve Adicionar Tarefa Quando Repositório Incluir com Sucesso.")]
    public void Should_AddTodo_WhenRepositoryIncludeSucess()
    {
        // Arrange
        var request = TodoFaker.GenerateRequestObject();
        var validatorMock = new Mock<IValidator<RequestTodoJson>>();
        var resultValidator = new ValidationResult();
        validatorMock.Setup(s => s.Validate(request)).Returns(resultValidator);
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.Add(It.IsAny<TodoViewModel>())).Returns(true);

        // Act
        var actual = new AddTodoUseCase(todoRespositoryMock.Object, validatorMock.Object).Execute(request);

        // Asserts
        actual.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status201Created);

        validatorMock.Verify(s => s.Validate(request), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.Add(It.IsAny<TodoViewModel>()), Times.Exactly(1));
    }
}
