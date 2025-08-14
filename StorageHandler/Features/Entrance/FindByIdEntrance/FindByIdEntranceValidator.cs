using FluentValidation;

namespace StorageHandler.Features.Entrance.FindByIdEntrance;

public class FindByIdEntranceValidator : AbstractValidator<FindByIdEntranceQuery>
{
    public FindByIdEntranceValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
    }
}