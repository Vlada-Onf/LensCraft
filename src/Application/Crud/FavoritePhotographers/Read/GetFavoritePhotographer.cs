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
    public enum GetFavoritePhotographerError
    {
        InvalidUserId,
        InvalidPhotographerId,
        NotFound
    }
    public record GetFavoritePhotographer(Guid UserId, Guid PhotographerId)
        : IRequest<Result<GetFavoritePhotographerError, UserFavoritePhotographer>>;
}
