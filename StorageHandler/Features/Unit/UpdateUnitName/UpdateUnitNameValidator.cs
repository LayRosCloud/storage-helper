using FluentValidation;

namespace StorageHandler.Features.Unit.UpdateUnitName;

public class UpdateUnitNameValidator : AbstractValidator<UpdateUnitNameCommand>
{
    public UpdateUnitNameValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1);
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(10);
    }
}