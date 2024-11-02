using FluentAssertions;
using FluentValidation.Results;
using TodoManager.Domain.Contracts.Requests;
using TodoManager.Domain.Tests.Faker;
using TodoManager.Domain.Validators;

namespace TodoManager.Domain.Tests.Validators;
public class RequestTodoValidatorTest
{
    public static IEnumerable<object[]> GenerateInValid()
    {
        var nameEmpty = new ValidationFailure("Name", "O nome é obrigatório.");
        var nameLenght = new ValidationFailure("Name", "O nome só pode ter de 2 a 100 caracteres.");
        var descriptionEmpty = new ValidationFailure("Description", "A descrição é obrigatório.");
        var descriptionLenght = new ValidationFailure("Description", "A descrição só pode ter de 3 a 300 caracteres.");

        yield return new object[] { new RequestTodoJson(), new[] {nameEmpty, nameLenght, descriptionEmpty, descriptionLenght } };
        yield return new object[] { new RequestTodoJson() { Name = null, Description = null }, new[] { nameEmpty, descriptionEmpty } };
        yield return new object[] { new RequestTodoJson() { Name = "A", Description="A" }, new[] { nameLenght, descriptionLenght } };
    }

    public static IEnumerable<object[]> GenerateValid()
    {
        var random = new Random();
        var list = TodoFaker.GenerateRequestList(random.Next(1, 11));
        foreach (var item in list)
        {
            yield return new object[] { item };
        }
    }

    [Fact(DisplayName = "Não Deve Validar Quando Receber um Objeto Nulo.")]
    public void Should_NotValidate_WhenReceivingNullObject()
    {
        // Arrange
        RequestTodoJson model = null;
        var validator = new RequestTodoValidator();
        var errorValidator = new ValidationFailure(nameof(RequestTodoJson), "O objeto da lista não pode ser nulo.");

        // Act
        var actual = validator.Validate(model);

        // Asserts
        actual.IsValid.Should().BeFalse();
        actual.Errors.Should().NotBeEmpty();
        actual.Errors.FirstOrDefault().Should().BeEquivalentTo(errorValidator);
    }

    //Não Deve Validar Quando Receber Dados Inválidos
    [Theory(DisplayName = "Não Deve Validar Quando Receber Dados Inválidos.")]
    [MemberData(nameof(GenerateInValid))]
    public void Should_NotValidate_WhenReceivingInvalidData(RequestTodoJson model, IEnumerable<ValidationFailure> expected)
    {
        // Arrange
        var validator = new RequestTodoValidator();

        // Act
        var actual = validator.Validate(model);

        // Asserts
        actual.IsValid.Should().BeFalse();
        actual.Errors.Count.Should().Be(expected.Count());
        foreach (var expectedError in expected)
        {
            var matchingError = actual.Errors.SingleOrDefault(e => e.PropertyName == expectedError.PropertyName && e.ErrorMessage == expectedError.ErrorMessage);
            matchingError.Should().NotBeNull();
        }
    }

    [Theory(DisplayName = "Deve Validar Quando Receber Dados Validos.")]
    [MemberData(nameof(GenerateValid))]
    public void Should_Validate_WhenReceivingValidData(RequestTodoJson model)
    {
        // Arrange
        var validator = new RequestTodoValidator();

        // Act
        var actual = validator.Validate(model);

        // Asserts
        actual.IsValid.Should().BeTrue();
        actual.Errors.Should().BeEmpty();
    }
}
