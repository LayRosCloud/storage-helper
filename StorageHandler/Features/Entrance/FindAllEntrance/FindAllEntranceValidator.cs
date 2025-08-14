using FluentValidation;

namespace StorageHandler.Features.Entrance.FindAllEntrance;

public class FindAllEntranceValidator : AbstractValidator<FindAllEntranceQuery>
{
    public FindAllEntranceValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.Page)
            .NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit)
            .NotEmpty().GreaterThan(0);
    }
}