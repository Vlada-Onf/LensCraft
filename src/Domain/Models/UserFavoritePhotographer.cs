using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserFavoritePhotographer
    {
        public Guid UserId { get; }
        public User User { get; }

        public Guid PhotographerId { get; }
        public Photographer Photographer { get; }

        private UserFavoritePhotographer(Guid userId, Guid photographerId)
        {
            UserId = userId;
            PhotographerId = photographerId;
        }

        public static UserFavoritePhotographer Create(Guid userId, Guid photographerId)
            => new UserFavoritePhotographer(userId, photographerId);
    }
}
