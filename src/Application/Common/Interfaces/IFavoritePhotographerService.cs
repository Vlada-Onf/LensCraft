using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IFavoritePhotographerService
    {
        Task<UserFavoritePhotographer?> GetByUserAndPhotographerAsync(Guid userId, Guid photographerId);
        Task<IReadOnlyList<UserFavoritePhotographer>> GetByUserIdAsync(Guid userId);
        Task<UserFavoritePhotographer?> AddAsync(Guid userId, Guid photographerId);
        Task<bool> RemoveAsync(Guid userId, Guid photographerId);
    }
}
