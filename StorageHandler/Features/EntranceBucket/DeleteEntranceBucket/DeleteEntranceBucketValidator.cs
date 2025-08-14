using FluentValidation;

namespace StorageHandler.Features.EntranceBucket.DeleteEntranceBucket;

public class DeleteEntranceBucketValidator : AbstractValidator<DeleteEntranceBucketCommand>
{
    public DeleteEntranceBucketValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
    }
}