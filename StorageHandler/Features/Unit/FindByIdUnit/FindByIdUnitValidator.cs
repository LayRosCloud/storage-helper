using FluentValidation;

namespace StorageHandler.Features.Unit.FindByIdUnit;

public class FindByIdUnitValidator : AbstractValidator<FindByIdUnitQuery>
{
    public FindByIdUnitValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1);
    }
}