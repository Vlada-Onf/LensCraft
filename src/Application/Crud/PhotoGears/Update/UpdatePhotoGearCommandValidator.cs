using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Crud.PhotoGears.Update
{
    public class UpdatePhotoGearCommandValidator : AbstractValidator<UpdatePhotoGearCommand>
    {
        public UpdatePhotoGearCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Id must not be empty");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required")
                .MinimumLength(3).WithMessage("Model must be at least 3 characters long")
                .MaximumLength(50).WithMessage("Model must not exceed 50 characters");
        }
    }
}
