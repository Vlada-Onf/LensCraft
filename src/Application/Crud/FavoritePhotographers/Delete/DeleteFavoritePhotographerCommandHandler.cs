using Application.Common.Interfaces;
using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.FavoritePhotographers.Delete
{
    public class DeleteFavoritePhotographerCommandHandler(IFavoritePhotographerService service)
        : IRequestHandler<DeleteFavoritePhotographerCommand, Result<DeleteFavoritePhotographerError, bool>>
    {
        public async Task<Result<DeleteFavoritePhotographerError, bool>> Handle(
            DeleteFavoritePhotographerCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
                return Result<DeleteFavoritePhotographerError, bool>.Fail(DeleteFavoritePhotographerError.InvalidUserId);

            if (request.PhotographerId == Guid.Empty)
                return Result<DeleteFavoritePhotographerError, bool>.Fail(DeleteFavoritePhotographerError.InvalidPhotographerId);

            var deleted = await service.RemoveAsync(request.UserId, request.PhotographerId);
            if (!deleted)
                return Result<DeleteFavoritePhotographerError, bool>.Fail(DeleteFavoritePhotographerError.NotFound);

            return Result<DeleteFavoritePhotographerError, bool>.Success(true);
        }
    }
}
