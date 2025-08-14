using FluentValidation;

namespace StorageHandler.Features.EntranceBucket.FindAllEntranceBucketById;

public class FindAllEntranceBucketByIdValidator : AbstractValidator<FindAllEntranceBucketByIdQuery>
{
    public FindAllEntranceBucketByIdValidator()
    {
        RuleFor(x => x.Limit)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.Page)
            .NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.EntranceId)
            .NotEmpty().GreaterThan(0);
    }
}