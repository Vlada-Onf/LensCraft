using Application.Common.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FavoritePhotographerService : IFavoritePhotographerService
    {
        private readonly ApplicationDbContext _db;

        public FavoritePhotographerService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UserFavoritePhotographer?> GetByUserAndPhotographerAsync(Guid userId, Guid photographerId)
        {
            return await _db.UserFavoritePhotographers
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PhotographerId == photographerId);
        }

        public async Task<IReadOnlyList<UserFavoritePhotographer>> GetByUserIdAsync(Guid userId)
        {
            return await _db.UserFavoritePhotographers
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserFavoritePhotographer?> AddAsync(Guid userId, Guid photographerId)
        {
            var exists = await _db.UserFavoritePhotographers
                .AnyAsync(f => f.UserId == userId && f.PhotographerId == photographerId);

            if (exists)
                return null;

            var favorite = UserFavoritePhotographer.Create(userId, photographerId);
            await _db.UserFavoritePhotographers.AddAsync(favorite);
            await _db.SaveChangesAsync();
            return favorite;
        }

        public async Task<bool> RemoveAsync(Guid userId, Guid photographerId)
        {
            var favorite = await _db.UserFavoritePhotographers
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PhotographerId == photographerId);

            if (favorite is null) return false;

            _db.UserFavoritePhotographers.Remove(favorite);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
