using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.Crud.Photographers.Read
{
    public class GetAllPhotographersHandler(IPhotographerService photographerService)
    : IRequestHandler<GetAllPhotographers, IReadOnlyList<Photographer>>
    {
        public async Task<IReadOnlyList<Photographer>> Handle(GetAllPhotographers request, CancellationToken cancellationToken)
        {
            return await photographerService.GetAllAsync();
        }
    }
}
