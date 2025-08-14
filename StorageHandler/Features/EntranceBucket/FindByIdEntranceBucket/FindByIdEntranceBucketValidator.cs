using FluentValidation;

namespace StorageHandler.Features.EntranceBucket.FindByIdEntranceBucket;

public class FindByIdEntranceBucketValidator : AbstractValidator<FindByIdEntranceBucketQuery>
{
    public FindByIdEntranceBucketValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
    }
}