using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Photographers.Read
{
    public class GetPhotographerByIdQueryHandler(
    IPhotographerService photographerService
) : IRequestHandler<GetPhotographerById, Photographer?>
    {
        public async Task<Photographer?> Handle(GetPhotographerById request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Id must not be empty");

            return await photographerService.GetByIdAsync(request.Id);
        }
    }
}
