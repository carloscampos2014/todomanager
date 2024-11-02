using FluentAssertions;
using TodoManager.Database.Sqlite.Repositories;
using TodoManager.Database.Sqlite.Tests.Faker;
using TodoManager.Database.Sqlite.Tests.Fixtures;
using TodoManager.Domain.Contracts.Dto;
using TodoManager.Domain.Contracts.Response;

namespace TodoManager.Database.Sqlite.Tests.Repositories;

[Collection("Database")]
public class TodoRepositoryTest
{
    private readonly DataBaseFixture _fixture;

    public TodoRepositoryTest(DataBaseFixture fixture)
    {
        _fixture = fixture;
        var responseAll = new ResponseAllTodoJson()
        {
            Todos = [],
        };

        var responseErrors = new ResponseErrorsJson()
        {
            Errors = []
        };

        Console.WriteLine(responseAll);
        Console.WriteLine(responseErrors);
    }

    [Fact(DisplayName = "Não Deve Adicionar Tarefa Quando Receber um Modelo Nulo.")]
    public void Should_NotAddTodo_WhenRecibeNullModel()
    {
        // Arrange
        _fixture.Clear();
        TodoViewModel model = null;
        var repository = new TodoRepository(_fixture.ConnectionFactory);

        // Act
        var actual = repository.Add(model);

        // Asserts
        actual.Should().BeFalse();
    }

    [Fact(DisplayName = "Deve Adicionar Tarefa Quando Receber um Modelo Valido.")]
    public void Should_AddTodo_WhenRecibeValidModel()
    {
        // Arrange
        _fixture.Clear();
        TodoViewModel model = TodoFaker.GenerateTodoObject();
        var repository = new TodoRepository(_fixture.ConnectionFactory);

        // Act
        var actual = repository.Add(model);
        var expected = repository.GetById(model.Id);

        // Asserts
        actual.Should().BeTrue();
        expected.Should().NotBeNull();
        expected.Should().BeEquivalentTo(model);
    }

    [Fact(DisplayName = "Deve Excluir Tarefa Quando Existir.")]
    public void Should_DeleteTodo_WhenExists()
    {
        // Arrange
        _fixture.Clear();
        TodoViewModel model = TodoFaker.GenerateTodoObject();
        _fixture.AddTodo(model);
        var repository = new TodoRepository(_fixture.ConnectionFactory);

        // Act
        var actual = repository.Delete(model.Id);
        var expected = repository.GetById(model.Id);

        // Asserts
        actual.Should().BeTrue();
        expected.Should().BeNull();
    }

    [Fact(DisplayName = "Deve Retornar Todas Tarefas Quando Existirem.")]
    public void Should_GetAllTodo_WhenExists()
    {
        // Arrange
        _fixture.Clear();
        TodoViewModel model = TodoFaker.GenerateTodoObject();
        _fixture.AddTodo(model);
        var repository = new TodoRepository(_fixture.ConnectionFactory);

        // Act
        var actual = repository.GetAll();

        // Asserts
        actual.Should().NotBeEmpty();
        actual.FirstOrDefault().Should().BeEquivalentTo(model);
    }

    [Fact(DisplayName = "Deve Retornar Tarefa pelo Id Quando Existir.")]
    public void Should_GetByIdTodo_WhenExists()
    {
        // Arrange
        _fixture.Clear();
        TodoViewModel model = TodoFaker.GenerateTodoObject();
        _fixture.AddTodo(model);
        var repository = new TodoRepository(_fixture.ConnectionFactory);

        // Act
        var actual = repository.GetById(model.Id);

        // Asserts
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(model);
    }

    [Fact(DisplayName = "Não Deve Alterar Tarefa Quando Receber um Modelo Nulo.")]
    public void Should_NotUpdateTodo_WhenRecibeNullModel()
    {
        // Arrange
        _fixture.Clear();
        TodoViewModel model = null;
        var repository = new TodoRepository(_fixture.ConnectionFactory);

        // Act
        var actual = repository.Update(model);

        // Asserts
        actual.Should().BeFalse();
    }

    [Fact(DisplayName = "Deve Alterar Tarefa Quando Receber um Modelo Valido.")]
    public void Should_UpdateTodo_WhenRecibeValidModel()
    {
        // Arrange
        _fixture.Clear();
        var model = TodoFaker.GenerateTodoObject();
        var modelUpdate = TodoFaker.GenerateTodoObject();
        _fixture.AddTodo(model);
        var repository = new TodoRepository(_fixture.ConnectionFactory);
        model.Name = modelUpdate.Name;
        model.Description = modelUpdate.Description;
        model.Deadline = modelUpdate.Deadline;
        model.Priority = modelUpdate.Priority;
        model.Status = modelUpdate.Status;

        // Act
        var actual = repository.Update(model);
        var expected = repository.GetById(model.Id);

        // Asserts
        actual.Should().BeTrue();
        expected.Should().NotBeNull();
        expected.Should().BeEquivalentTo(model);
    }
}
