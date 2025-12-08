using Application.Common.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class PhotographerService : IPhotographerService
{
    private readonly ApplicationDbContext _db;

    public PhotographerService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Photographer> CreateAsync(Guid id, string firstName, string lastName, string email, string bio)
    {
        var photographer = Photographer.Create(id, firstName, lastName, email, bio);
        _db.Photographers.Add(photographer);
        await _db.SaveChangesAsync();
        return photographer;
    }

    public async Task<Photographer?> GetByIdAsync(Guid id)
    {
        return await _db.Photographers.FindAsync(id);
    }

    public async Task<IReadOnlyList<Photographer>> GetAllAsync()
    {
        return await _db.Photographers.ToListAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var photographer = await _db.Photographers.FindAsync(id);
        if (photographer is null) return false;

        _db.Photographers.Remove(photographer);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}


