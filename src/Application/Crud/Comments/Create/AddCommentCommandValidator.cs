using FluentValidation;

namespace Application.Crud.Comments.Create;

public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentCommandValidator()
    {
        RuleFor(x => x.PortfolioItemId)
            .NotEqual(Guid.Empty).WithMessage("PortfolioItemId must not be empty");

        RuleFor(x => x.AuthorId)
            .NotEqual(Guid.Empty).WithMessage("AuthorId must not be empty");

        RuleFor(x => x.Text)
            .NotNull().WithMessage("Text cannot be null")
            .NotEmpty().WithMessage("Text is required")
            .MinimumLength(1).WithMessage("Text must be at least 1 character long"); // Змінив з 3 на 1
    }
}
