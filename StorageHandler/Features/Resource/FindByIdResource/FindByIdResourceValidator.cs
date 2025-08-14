using FluentValidation;

namespace StorageHandler.Features.Resource.FindByIdResource;

public class FindByIdResourceValidator : AbstractValidator<FindByIdResourceQuery>
{
    public FindByIdResourceValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
    }
}