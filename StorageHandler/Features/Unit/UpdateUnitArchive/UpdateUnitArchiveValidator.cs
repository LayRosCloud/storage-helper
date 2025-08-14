using FluentValidation;

namespace StorageHandler.Features.Unit.UpdateUnitArchive;

public class UpdateUnitArchiveValidator : AbstractValidator<UpdateUnitArchiveCommand>
{
    public UpdateUnitArchiveValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
    }
}