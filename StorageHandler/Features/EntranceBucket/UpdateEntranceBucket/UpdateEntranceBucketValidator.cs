using FluentValidation;

namespace StorageHandler.Features.EntranceBucket.UpdateEntranceBucket;

public class UpdateEntranceBucketValidator : AbstractValidator<UpdateEntranceBucketCommand>
{
    public UpdateEntranceBucketValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.Quantity)
            .NotEmpty().GreaterThan(0);
    }
}