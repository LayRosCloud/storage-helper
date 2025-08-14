using FluentValidation;
using StorageHandler.Features.EntranceBucket.CreateEntranceBucket;

namespace StorageHandler.Features.EntranceBucket.CreateRangeEntranceBucket;

public class CreateRangeEntranceBucketValidator : AbstractValidator<CreateRangeEntranceBucketCommand>
{
    public CreateRangeEntranceBucketValidator()
    {
        RuleFor(x => x.Buckets)
            .NotEmpty()
            .ForEach(x => x.SetValidator(new CreateEntranceBucketValidator()));
    }
}