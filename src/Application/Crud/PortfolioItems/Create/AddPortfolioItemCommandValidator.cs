using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PortfolioItems.Create
{
    public class AddPortfolioItemCommandValidator : AbstractValidator<AddPortfolioItemCommand>
    {
        public AddPortfolioItemCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters long");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MinimumLength(3).WithMessage("Description must be at least 3 characters long");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image URL is required")
                .Must(url => url.StartsWith("http")).WithMessage("Image URL must be valid");

            RuleFor(x => x.PhotographerId)
                .NotEqual(Guid.Empty).WithMessage("PhotographerId must not be empty");
        }
    }
}
