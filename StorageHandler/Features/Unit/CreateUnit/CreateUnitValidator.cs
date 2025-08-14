using FluentValidation;

namespace StorageHandler.Features.Unit.CreateUnit;

public class CreateUnitValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitValidator()
    {
        RuleFor(x=>x.Name)
            .NotEmpty().MaximumLength(10);
    }
}