using FluentValidation;

namespace StorageHandler.Features.EntranceBucket.CreateEntranceBucket;

public class CreateEntranceBucketValidator : AbstractValidator<CreateEntranceBucketCommand>
{
    public CreateEntranceBucketValidator()
    {
        RuleFor(x => x.UnitId)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.Quantity)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.EntranceId)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.ResourceId)
            .NotEmpty().GreaterThan(0);
    }
}