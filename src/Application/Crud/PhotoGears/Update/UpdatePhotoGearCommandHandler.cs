using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotoGears.Update
{
    public class UpdatePhotoGearCommandHandler(IPhotoGearService photoGearService)
    : IRequestHandler<UpdatePhotoGearCommand, Result<UpdatePhotoGearError, PhotoGear>>
    {
        public async Task<Result<UpdatePhotoGearError, PhotoGear>> Handle(
            UpdatePhotoGearCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<UpdatePhotoGearError, PhotoGear>.Fail(UpdatePhotoGearError.InvalidId);

            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<UpdatePhotoGearError, PhotoGear>.Fail(UpdatePhotoGearError.InvalidName);

            if (string.IsNullOrWhiteSpace(request.Model))
                return Result<UpdatePhotoGearError, PhotoGear>.Fail(UpdatePhotoGearError.InvalidModel);

            var gear = await photoGearService.GetByIdAsync(request.Id);
            if (gear is null)
                return Result<UpdatePhotoGearError, PhotoGear>.Fail(UpdatePhotoGearError.NotFound);

            gear.UpdateDetails(request.Name, request.Model);
            gear.ChangeStatus(request.Status);
            await photoGearService.SaveAsync();

            return Result<UpdatePhotoGearError, PhotoGear>.Success(gear);
        }
    }
}
