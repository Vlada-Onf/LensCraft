using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotographerProfiles.Create
{
    public class CreatePhotographerProfileCommandHandler(IPhotographerProfileService service)
        : IRequestHandler<CreatePhotographerProfileCommand, Result<CreatePhotographerProfileError, PhotographerProfile>>
    {
        public async Task<Result<CreatePhotographerProfileError, PhotographerProfile>> Handle(
            CreatePhotographerProfileCommand request,
            CancellationToken cancellationToken)
        {
            if (request.PhotographerId == Guid.Empty)
                return Result<CreatePhotographerProfileError, PhotographerProfile>.Fail(CreatePhotographerProfileError.InvalidPhotographerId);

            var profile = await service.CreateAsync(request.PhotographerId, request.Phone, request.Website, request.Instagram);
            return Result<CreatePhotographerProfileError, PhotographerProfile>.Success(profile);
        }
    }
}
