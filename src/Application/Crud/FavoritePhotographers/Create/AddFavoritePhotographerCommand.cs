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
    public enum AddFavoritePhotographerError
    {
        InvalidUserId,
        InvalidPhotographerId,
        AlreadyFavorited
    }
    public record AddFavoritePhotographerCommand : IRequest<Result<AddFavoritePhotographerError, UserFavoritePhotographer>>
    {
        public required Guid UserId { get; init; }
        public required Guid PhotographerId { get; init; }
    }
}
