using FluentValidation;

namespace StorageHandler.Features.Unit.UpdateUnitName;

public class UpdateUnitNameValidator : AbstractValidator<UpdateUnitNameCommand>
{
    public UpdateUnitNameValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(10);
    }
}