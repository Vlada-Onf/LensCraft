using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Photographers.Create
{
    public class AddPhotographerCommandValidator : AbstractValidator<AddPhotographerCommand>
    {
        public AddPhotographerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MinimumLength(3).WithMessage("First name must be at least 3 characters long");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MinimumLength(3).WithMessage("Last name must be at least 3 characters long");

            RuleFor(x => x.Bio)
                .NotEmpty().WithMessage("Bio is required")
                .MinimumLength(10).WithMessage("Bio must be at least 10 characters long");
        }
    }
}
