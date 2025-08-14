using FluentValidation;

namespace StorageHandler.Features.Resource.DeleteResourceById;

public class DeleteResourceByIdValidator : AbstractValidator<DeleteResourceByIdCommand>
{
    public DeleteResourceByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0);
    }
}