using Application.Common.Interfaces;
using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Photographers.Delete
{
    public class DeletePhotographerCommandHandler
    : IRequestHandler<DeletePhotographerCommand, Result<DeletePhotographerError, bool>>
    {
        private readonly IPhotographerService _service;

        public DeletePhotographerCommandHandler(IPhotographerService service)
        {
            _service = service;
        }

        public async Task<Result<DeletePhotographerError, bool>> Handle(
            DeletePhotographerCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<DeletePhotographerError, bool>.Fail(DeletePhotographerError.InvalidId);

            var deleted = await _service.DeleteAsync(request.Id);
            // або твій існуючий метод RemoveAsync / DeleteAsync

            if (!deleted)
                return Result<DeletePhotographerError, bool>.Fail(DeletePhotographerError.NotFound);

            return Result<DeletePhotographerError, bool>.Success(true);
        }
    }
}
