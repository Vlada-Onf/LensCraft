using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotographerProfiles.Update
{
    public class UpdatePhotographerProfileCommandHandler(IPhotographerProfileService service)
        : IRequestHandler<UpdatePhotographerProfileCommand, Result<UpdatePhotographerProfileError, PhotographerProfile>>
    {
        public async Task<Result<UpdatePhotographerProfileError, PhotographerProfile>> Handle(
            UpdatePhotographerProfileCommand request,
            CancellationToken cancellationToken)
        {
            if (request.PhotographerId == Guid.Empty)
                return Result<UpdatePhotographerProfileError, PhotographerProfile>.Fail(UpdatePhotographerProfileError.InvalidPhotographerId);

            var profile = await service.GetByPhotographerIdAsync(request.PhotographerId);
            if (profile is null)
                return Result<UpdatePhotographerProfileError, PhotographerProfile>.Fail(UpdatePhotographerProfileError.NotFound);

            profile.Update(request.Phone, request.Website, request.Instagram);
            await service.SaveAsync();

            return Result<UpdatePhotographerProfileError, PhotographerProfile>.Success(profile);
        }
    }
}
