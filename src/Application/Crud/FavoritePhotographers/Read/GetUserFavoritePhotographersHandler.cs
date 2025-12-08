using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.FavoritePhotographers.Read
{
    public class GetUserFavoritePhotographersHandler(IFavoritePhotographerService service)
        : IRequestHandler<GetUserFavoritePhotographers, IReadOnlyList<UserFavoritePhotographer>>
    {
        public async Task<IReadOnlyList<UserFavoritePhotographer>> Handle(
            GetUserFavoritePhotographers request,
            CancellationToken cancellationToken)
        {
            return await service.GetByUserIdAsync(request.UserId);
        }
    }
}
