using FluentValidation;

namespace Application.Crud.Comments.Update;

public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
{
    public EditCommentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Comment ID must not be empty");

        RuleFor(x => x.NewText)
            .NotNull().WithMessage("Text cannot be null")
            .NotEmpty().WithMessage("Text is required")
            .MinimumLength(1).WithMessage("Text must be at least 1 character long"); // Змінив з 3 на 1
    }
}
