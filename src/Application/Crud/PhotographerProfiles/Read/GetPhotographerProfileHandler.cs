using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotographerProfiles.Read
{
    public class GetPhotographerProfileHandler(IPhotographerProfileService service)
        : IRequestHandler<GetPhotographerProfile, Result<GetPhotographerProfileError, PhotographerProfile>>
    {
        public async Task<Result<GetPhotographerProfileError, PhotographerProfile>> Handle(
            GetPhotographerProfile request,
            CancellationToken cancellationToken)
        {
            if (request.PhotographerId == Guid.Empty)
                return Result<GetPhotographerProfileError, PhotographerProfile>.Fail(GetPhotographerProfileError.InvalidPhotographerId);

            var profile = await service.GetByPhotographerIdAsync(request.PhotographerId);
            if (profile is null)
                return Result<GetPhotographerProfileError, PhotographerProfile>.Fail(GetPhotographerProfileError.NotFound);

            return Result<GetPhotographerProfileError, PhotographerProfile>.Success(profile);
        }
    }
}
