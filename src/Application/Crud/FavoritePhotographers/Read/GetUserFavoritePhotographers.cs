using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.FavoritePhotographers.Read
{
    public record GetUserFavoritePhotographers(Guid UserId) : IRequest<IReadOnlyList<UserFavoritePhotographer>>;
}
