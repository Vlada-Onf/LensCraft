using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Crud.PhotoGears.Read
{
    public class GetPhotoGearByIdHandler(
        IPhotoGearService photoGearService
    ) : IRequestHandler<GetPhotoGearById, PhotoGear?>
    {
        public async Task<PhotoGear?> Handle(GetPhotoGearById request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Id must not be empty");

            return await photoGearService.GetByIdAsync(request.Id);
        }
    }

}
