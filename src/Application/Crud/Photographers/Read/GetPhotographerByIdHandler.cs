using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Photographers.Read
{
    public class GetPhotographerByIdHandler
    : IRequestHandler<GetPhotographerById, Result<GetPhotographerByIdError, Photographer>>
    {
        private readonly IPhotographerService _service;

        public GetPhotographerByIdHandler(IPhotographerService service)
        {
            _service = service;
        }

        public async Task<Result<GetPhotographerByIdError, Photographer>> Handle(
            GetPhotographerById request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<GetPhotographerByIdError, Photographer>.Fail(GetPhotographerByIdError.InvalidId);

            var photographer = await _service.GetByIdAsync(request.Id);

            if (photographer is null)
                return Result<GetPhotographerByIdError, Photographer>.Fail(GetPhotographerByIdError.NotFound);

            return Result<GetPhotographerByIdError, Photographer>.Success(photographer);
        }
    }

}
