using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Crud.PhotoGears.Update
{
    public class UpdatePhotoGearCommandHandler(
        IPhotoGearService photoGearService
    ) : IRequestHandler<UpdatePhotoGearCommand, PhotoGear>
    {
        public async Task<PhotoGear> Handle(UpdatePhotoGearCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Id must not be empty");

            var gear = await photoGearService.GetByIdAsync(request.Id);
            if (gear is null)
                throw new KeyNotFoundException($"PhotoGear with ID {request.Id} not found");

            gear.UpdateDetails(request.Name, request.Model);
            gear.ChangeStatus(request.Status); 

            await photoGearService.SaveAsync();

            return gear;
        }
    }
}
