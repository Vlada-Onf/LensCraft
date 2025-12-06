using FluentValidation;

namespace Application.Crud.Photographers.Update;

public class UpdatePhotographerCommandValidator : AbstractValidator<UpdatePhotographerCommand>
{
    public UpdatePhotographerCommandValidator()
    {
        // НЕ ВАЛІДУЙ ID! Це дозволить Handler перевірити чи photographer існує
        // RuleFor(x => x.Id).NotEqual(Guid.Empty); ← НЕ ДОДАВАЙ ЦЕ!

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required")
            .MaximumLength(100).WithMessage("FirstName must not exceed 100 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .MaximumLength(100).WithMessage("LastName must not exceed 100 characters");

        RuleFor(x => x.Bio)
            .NotEmpty().WithMessage("Bio is required")
            .MaximumLength(1000).WithMessage("Bio must not exceed 1000 characters");
    }
}
