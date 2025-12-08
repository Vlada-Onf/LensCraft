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
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _db;

        public GenreService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Genre?> GetByIdAsync(Guid id)
        {
            return await _db.Genres.FindAsync(id);
        }

        public async Task<IReadOnlyList<Genre>> GetAllAsync()
        {
            return await _db.Genres.ToListAsync();
        }

        public async Task<Genre> CreateAsync(Guid id, string name)
        {
            var genre = Genre.Create(id, name);
            _db.Genres.Add(genre);
            await _db.SaveChangesAsync();
            return genre;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var genre = await _db.Genres.FindAsync(id);
            if (genre is null) return false;

            _db.Genres.Remove(genre);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task SaveAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
