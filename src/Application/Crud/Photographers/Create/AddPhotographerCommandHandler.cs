using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Photographers.Create
{
    public class AddPhotographerCommandHandler
    : IRequestHandler<AddPhotographerCommand, Result<AddPhotographerError, Photographer>>
    {
        private readonly IPhotographerService _service;

        public AddPhotographerCommandHandler(IPhotographerService service)
        {
            _service = service;
        }

        public async Task<Result<AddPhotographerError, Photographer>> Handle(
            AddPhotographerCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
                return Result<AddPhotographerError, Photographer>.Fail(AddPhotographerError.InvalidFirstName);

            if (string.IsNullOrWhiteSpace(request.LastName))
                return Result<AddPhotographerError, Photographer>.Fail(AddPhotographerError.InvalidLastName);

            if (string.IsNullOrWhiteSpace(request.Email))
                return Result<AddPhotographerError, Photographer>.Fail(AddPhotographerError.InvalidEmail);

            var photographer = await _service.CreateAsync(
                Guid.NewGuid(),
                request.FirstName,
                request.LastName,
                request.Email,
                request.Bio);

            return Result<AddPhotographerError, Photographer>.Success(photographer);
        }
    }
}
