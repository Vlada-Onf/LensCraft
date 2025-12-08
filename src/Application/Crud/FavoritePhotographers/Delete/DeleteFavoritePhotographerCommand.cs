using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.FavoritePhotographers.Delete
{
    public enum DeleteFavoritePhotographerError
    {
        InvalidUserId,
        InvalidPhotographerId,
        NotFound
    }
    public record DeleteFavoritePhotographerCommand(Guid UserId, Guid PhotographerId)
        : IRequest<Result<DeleteFavoritePhotographerError, bool>>;
}
