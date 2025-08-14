using FluentValidation;

namespace StorageHandler.Features.Resource.UpdateResourceName;

public class UpdateResourceNameValidator : AbstractValidator<UpdateResourceNameCommand>
{
    public UpdateResourceNameValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}