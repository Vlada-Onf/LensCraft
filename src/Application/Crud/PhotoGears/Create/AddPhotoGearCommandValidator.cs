using FluentValidation;

namespace Application.Crud.PhotoGears.Create;

public class AddPhotoGearCommandValidator : AbstractValidator<AddPhotoGearCommand>
{
    public AddPhotoGearCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required")
            .MinimumLength(3).WithMessage("Type must be at least 3 characters long");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required")
            .MinimumLength(3).WithMessage("Model must be at least 3 characters long");

        RuleFor(x => x.SerialNumber)
            .NotEmpty().WithMessage("Serial number is required")
            .MinimumLength(5).WithMessage("Serial number must be at least 5 characters long");

        RuleFor(x => x.PhotographerId)
            .NotEqual(Guid.Empty).WithMessage("PhotographerId must not be empty");
    }
}