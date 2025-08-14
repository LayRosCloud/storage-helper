using FluentValidation;

namespace StorageHandler.Features.Entrance.DeleteEntrance;

public class DeleteEntranceValidator : AbstractValidator<DeleteEntranceCommand>
{
    public DeleteEntranceValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
    }
}