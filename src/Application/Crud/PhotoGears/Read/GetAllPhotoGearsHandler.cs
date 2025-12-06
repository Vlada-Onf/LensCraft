using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotoGears.Read
{
    public class GetAllPhotoGearsHandler(IPhotoGearService photoGearService)
    : IRequestHandler<GetAllPhotoGears, IReadOnlyList<PhotoGear>>
    {
        public async Task<IReadOnlyList<PhotoGear>> Handle(GetAllPhotoGears request, CancellationToken cancellationToken)
        {
            return await photoGearService.GetAllAsync();
        }
    }
}
