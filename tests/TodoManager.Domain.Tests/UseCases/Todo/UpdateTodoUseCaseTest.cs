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

namespace TodoManager.Domain.Tests.UseCases.Todo;

public class UpdateTodoUseCaseTest
{
    [Fact(DisplayName = "Não Deve Atualizar Tarefa Quando a Validação Falhar.")]
    public void Should_NotUpdateTodo_WhenValidationFail()
    {
        // Arrange
        RequestTodoJson request = TodoFaker.GenerateRequestObject();
        TodoViewModel model = TodoFaker.GenerateTodoObject();
        var validatorMock = new Mock<IValidator<RequestTodoJson>>();
        var resultValidator = new ValidationResult(new List<ValidationFailure>() { new ValidationFailure(nameof(RequestTodoJson), "O objeto da lista não pode ser nulo.") });
        validatorMock.Setup(s => s.Validate(request)).Returns(resultValidator);
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetById(model.Id)).Returns(model);
        todoRespositoryMock.Setup(s => s.Update(It.IsAny<TodoViewModel>())).Returns(true);
        IUpdateTodoUseCase useCase = new UpdateTodoUseCase(todoRespositoryMock.Object, validatorMock.Object);

        // Act
        IActionResult actual = useCase.Execute(model.Id, request);

        // Asserts
        actual.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        validatorMock.Verify(s => s.Validate(request), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.GetById(model.Id), Times.Never);
        todoRespositoryMock.Verify(s => s.Update(It.IsAny<TodoViewModel>()), Times.Never);
    }

    [Fact(DisplayName = "Não Deve Atualizar Tarefa Quando Repositório Não Encontrar Registro.")]
    public void Should_NotUpdateTodo_WhenRepositoryNotFoundRegister()
    {
        // Arrange
        RequestTodoJson request = TodoFaker.GenerateRequestObject();
        Guid id = Guid.NewGuid();
        TodoViewModel model = null;
        var validatorMock = new Mock<IValidator<RequestTodoJson>>();
        var resultValidator = new ValidationResult();
        validatorMock.Setup(s => s.Validate(request)).Returns(resultValidator);
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetById(id)).Returns(model);
        todoRespositoryMock.Setup(s => s.Update(It.IsAny<TodoViewModel>())).Returns(false);
        IUpdateTodoUseCase useCase = new UpdateTodoUseCase(todoRespositoryMock.Object, validatorMock.Object);

        // Act
        IActionResult actual = useCase.Execute(id, request);

        // Asserts
        actual.Should().BeOfType<NotFoundObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status404NotFound);

        validatorMock.Verify(s => s.Validate(request), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.GetById(id), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.Update(It.IsAny<TodoViewModel>()), Times.Never);
    }

    [Fact(DisplayName = "Não Deve Atualizar Tarefa Quando Repositório Falhar na Inclusão.")]
    public void Should_NotUpdateTodo_WhenRepositoryFailUpdate()
    {
        // Arrange
        RequestTodoJson request = TodoFaker.GenerateRequestObject();
        TodoViewModel model = TodoFaker.GenerateTodoObject();
        var validatorMock = new Mock<IValidator<RequestTodoJson>>();
        var resultValidator = new ValidationResult();
        validatorMock.Setup(s => s.Validate(request)).Returns(resultValidator);
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetById(model.Id)).Returns(model);
        todoRespositoryMock.Setup(s => s.Update(It.IsAny<TodoViewModel>())).Returns(false);
        IUpdateTodoUseCase useCase = new UpdateTodoUseCase(todoRespositoryMock.Object, validatorMock.Object);

        // Act
        IActionResult actual = useCase.Execute(model.Id, request);

        // Asserts
        actual.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

        validatorMock.Verify(s => s.Validate(request), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.GetById(model.Id), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.Update(It.IsAny<TodoViewModel>()), Times.Exactly(1));
    }

    [Fact(DisplayName = "Deve Atualizar Tarefa Quando Repositório Alterar com Sucesso.")]
    public void Should_UpdateTodo_WhenRepositoryUpdateSucess()
    {
        // Arrange
        RequestTodoJson request = TodoFaker.GenerateRequestObject();
        TodoViewModel model = TodoFaker.GenerateTodoObject();
        var validatorMock = new Mock<IValidator<RequestTodoJson>>();
        var resultValidator = new ValidationResult();
        validatorMock.Setup(s => s.Validate(request)).Returns(resultValidator);
        var todoRespositoryMock = new Mock<ITodoRepository>();
        todoRespositoryMock.Setup(s => s.GetById(model.Id)).Returns(model);
        todoRespositoryMock.Setup(s => s.Update(It.IsAny<TodoViewModel>())).Returns(true);
        IUpdateTodoUseCase useCase = new UpdateTodoUseCase(todoRespositoryMock.Object, validatorMock.Object);

        // Act
        IActionResult actual = useCase.Execute(model.Id, request);

        // Asserts
        actual.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status200OK);

        validatorMock.Verify(s => s.Validate(request), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.GetById(model.Id), Times.Exactly(1));
        todoRespositoryMock.Verify(s => s.Update(It.IsAny<TodoViewModel>()), Times.Exactly(1));
    }
}
