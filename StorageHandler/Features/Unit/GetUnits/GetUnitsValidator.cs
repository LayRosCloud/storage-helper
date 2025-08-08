using FluentValidation;

namespace StorageHandler.Features.Unit.GetUnits;

public class GetUnitsValidator : AbstractValidator<GetUnitsQuery>
{
    public GetUnitsValidator()
    {
        RuleFor(x => x.Limit).GreaterThanOrEqualTo(1);
        RuleFor(x => x.Page).GreaterThanOrEqualTo(0);
    }
}