using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotoGears.Delete
{
    public class DeletePhotoGearCommandHandler(IPhotoGearService photoGearService)
        : IRequestHandler<DeletePhotoGearCommand, Result<DeletePhotoGearError, bool>>
    {
        public async Task<Result<DeletePhotoGearError, bool>> Handle(
            DeletePhotoGearCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<DeletePhotoGearError, bool>.Fail(DeletePhotoGearError.InvalidId);

            var deleted = await photoGearService.RemoveAsync(request.Id);
            if (!deleted)
                return Result<DeletePhotoGearError, bool>.Fail(DeletePhotoGearError.NotFound);

            return Result<DeletePhotoGearError, bool>.Success(true);
        }
    }
}
