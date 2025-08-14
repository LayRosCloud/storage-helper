using FluentValidation;
using StorageHandler.Features.EntranceBucket.CreateEntranceBucket;

namespace StorageHandler.Features.Entrance.CreateEntrance;

public class CreateEntranceValidator : AbstractValidator<CreateEntranceCommand>
{
    public CreateEntranceValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty().MaximumLength(30);
        RuleFor(x => x.Buckets)
            .NotEmpty()
            .ForEach(x => x.SetValidator(new CreateEntranceBucketValidator()));
    }
}