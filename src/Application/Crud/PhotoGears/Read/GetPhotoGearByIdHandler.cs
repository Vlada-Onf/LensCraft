using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotoGears.Read
{
    public class GetPhotoGearByIdHandler(IPhotoGearService photoGearService)
    : IRequestHandler<GetPhotoGearById, Result<GetPhotoGearByIdError, PhotoGear>>
    {
        public async Task<Result<GetPhotoGearByIdError, PhotoGear>> Handle(
            GetPhotoGearById request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<GetPhotoGearByIdError, PhotoGear>.Fail(GetPhotoGearByIdError.InvalidId);

            var gear = await photoGearService.GetByIdAsync(request.Id);
            if (gear is null)
                return Result<GetPhotoGearByIdError, PhotoGear>.Fail(GetPhotoGearByIdError.NotFound);

            return Result<GetPhotoGearByIdError, PhotoGear>.Success(gear);
        }
    }
}
