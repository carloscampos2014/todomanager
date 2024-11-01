using FluentValidation;
using FluentValidation.Results;
using TodoManager.Domain.Contracts.Requests;

namespace TodoManager.Domain.Validators;
public class RequestTodoValidator : AbstractValidator<RequestTodoJson>
{
    public RequestTodoValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(2, 100).WithMessage("O nome só pode ter de 2 a 100 caracteres.");

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage("A descrição é obrigatório.")
            .Length(3, 300).WithMessage("A descrição só pode ter de 3 a 300 caracteres.");
    }

    public override ValidationResult Validate(ValidationContext<RequestTodoJson> context)
    {
        if (context is null || context.InstanceToValidate is null)
        {
            return new ValidationResult(new List<ValidationFailure>() { new ValidationFailure(nameof(RequestTodoJson), "O objeto da lista não pode ser nulo.") });
        }

        return base.Validate(context);
    }
}
