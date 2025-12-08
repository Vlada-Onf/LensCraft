using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.FavoritePhotographers.Create
{
    public class AddFavoritePhotographerCommandHandler(IFavoritePhotographerService service)
        : IRequestHandler<AddFavoritePhotographerCommand, Result<AddFavoritePhotographerError, UserFavoritePhotographer>>
    {
        public async Task<Result<AddFavoritePhotographerError, UserFavoritePhotographer>> Handle(
            AddFavoritePhotographerCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
                return Result<AddFavoritePhotographerError, UserFavoritePhotographer>.Fail(AddFavoritePhotographerError.InvalidUserId);

            if (request.PhotographerId == Guid.Empty)
                return Result<AddFavoritePhotographerError, UserFavoritePhotographer>.Fail(AddFavoritePhotographerError.InvalidPhotographerId);

            var favorite = await service.AddAsync(request.UserId, request.PhotographerId);
            if (favorite == null)
                return Result<AddFavoritePhotographerError, UserFavoritePhotographer>.Fail(AddFavoritePhotographerError.AlreadyFavorited);

            return Result<AddFavoritePhotographerError, UserFavoritePhotographer>.Success(favorite);
        }
    }
}
