using FluentValidation;

namespace StorageHandler.Features.Entrance.UpdateEntrance;

public class UpdateEntranceValidator : AbstractValidator<UpdateEntranceCommand>
{
    public UpdateEntranceValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.Number)
            .NotEmpty()
            .MaximumLength(30);
    }
}