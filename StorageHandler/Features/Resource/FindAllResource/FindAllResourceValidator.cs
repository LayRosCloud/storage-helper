using FluentValidation;

namespace StorageHandler.Features.Resource.FindAllResource;

public class FindAllResourceValidator : AbstractValidator<FindAllResourcesQuery>
{
    public FindAllResourceValidator()
    {
        RuleFor(x => x.Limit)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.Page)
            .NotNull().GreaterThanOrEqualTo(0);
    }
}