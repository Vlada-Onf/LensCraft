using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Photographers.Create
{
    public class AddPhotographerCommandHandler(
     IPhotographerService photographerService
 ) : IRequestHandler<AddPhotographerCommand, Photographer>
    {
        public async Task<Photographer> Handle(AddPhotographerCommand request, CancellationToken cancellationToken)
        {
            var photographer = await photographerService.RegisterAsync(
                Guid.NewGuid(),
                request.FirstName,
                request.LastName,
                request.Email,
                request.Bio
            );

            return photographer;
        }
    }

}
