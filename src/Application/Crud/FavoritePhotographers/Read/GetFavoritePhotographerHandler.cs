using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.FavoritePhotographers.Read
{
    public class GetFavoritePhotographerHandler(IFavoritePhotographerService service)
        : IRequestHandler<GetFavoritePhotographer, Result<GetFavoritePhotographerError, UserFavoritePhotographer>>
    {
        public async Task<Result<GetFavoritePhotographerError, UserFavoritePhotographer>> Handle(
            GetFavoritePhotographer request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
                return Result<GetFavoritePhotographerError, UserFavoritePhotographer>.Fail(GetFavoritePhotographerError.InvalidUserId);

            if (request.PhotographerId == Guid.Empty)
                return Result<GetFavoritePhotographerError, UserFavoritePhotographer>.Fail(GetFavoritePhotographerError.InvalidPhotographerId);

            var favorite = await service.GetByUserAndPhotographerAsync(request.UserId, request.PhotographerId);
            if (favorite is null)
                return Result<GetFavoritePhotographerError, UserFavoritePhotographer>.Fail(GetFavoritePhotographerError.NotFound);

            return Result<GetFavoritePhotographerError, UserFavoritePhotographer>.Success(favorite);
        }
    }
}
