using Application.Common.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class PortfolioService : IPortfolioService
{
    private readonly ApplicationDbContext _db;

    public PortfolioService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PortfolioItem> AddAsync(Guid id, Guid photographerId, string title, string description, string imageUrl, string? location)
    {
        var exists = await _db.Photographers.AnyAsync(p => p.Id == photographerId);
        if (!exists)
            throw new ArgumentException("Photographer not found");

        var item = PortfolioItem.Create(id, photographerId, title, description, imageUrl, location);
        _db.PortfolioItems.Add(item);
        await _db.SaveChangesAsync();
        return item;
    }

    public async Task<PortfolioItem?> GetByIdAsync(Guid id)
    {
        return await _db.PortfolioItems.FindAsync(id);
    }

    public async Task<IReadOnlyList<PortfolioItem>> GetAllAsync()
    {
        return await _db.PortfolioItems.ToListAsync();
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        var item = await _db.PortfolioItems.FindAsync(id);
        if (item is null) return false;
        _db.PortfolioItems.Remove(item);
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}
