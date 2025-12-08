using Domain.Models;

namespace Api.Dtos
{
    public record FavoritePhotographerDto(Guid UserId, Guid PhotographerId)
    {
        public static FavoritePhotographerDto FromDomainModel(UserFavoritePhotographer favorite)
            => new(favorite.UserId, favorite.PhotographerId);
    }
    
    public record AddFavoritePhotographerDto(Guid UserId, Guid PhotographerId);
}
