using FluentValidation;

namespace StorageHandler.Features.Unit.DeleteUnit;

public class DeleteByIdUnitValidator : AbstractValidator<DeleteByIdUnitCommand>
{
    public DeleteByIdUnitValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
    }
}