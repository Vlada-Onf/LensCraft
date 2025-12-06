using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Crud.PhotoGears.Delete
{

    public class DeletePhotoGearCommandHandler(
        IPhotoGearService photoGearService
    ) : IRequestHandler<DeletePhotoGearCommand, bool>
    {
        public async Task<bool> Handle(DeletePhotoGearCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Id must not be empty");

            var gear = await photoGearService.GetByIdAsync(request.Id);
            if (gear is null)
                return false;

            return await photoGearService.RemoveAsync(gear.Id);
        }
    }
}
