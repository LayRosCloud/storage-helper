using FluentValidation;

namespace StorageHandler.Features.Resource.UpdateResourceArchive;

public class UpdateResourceArchiveValidator : AbstractValidator<UpdateResourceArchiveCommand>
{
    public UpdateResourceArchiveValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
    }
}