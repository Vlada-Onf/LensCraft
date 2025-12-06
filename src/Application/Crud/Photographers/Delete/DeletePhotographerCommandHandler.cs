using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Photographers.Delete
{
    public class DeletePhotographerCommandHandler(
    IPhotographerService photographerService
) : IRequestHandler<DeletePhotographerCommand, bool>
    {
        public async Task<bool> Handle(DeletePhotographerCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Id must not be empty");

            return await photographerService.RemoveAsync(request.Id);
        }
    }
}
