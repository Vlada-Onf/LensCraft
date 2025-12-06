using Application.Common.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class PhotoGearService : IPhotoGearService
{
    private readonly ApplicationDbContext _db;

    public PhotoGearService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PhotoGear> AddAsync(Guid id, Guid photographerId, string name, string type, string model, string serialNumber)
    {
        var photographerExists = await _db.Photographers.AnyAsync(p => p.Id == photographerId);
        if (!photographerExists)
            throw new ArgumentException($"Photographer with ID {photographerId} does not exist");

        var gear = PhotoGear.New(id, photographerId, name, type, model, serialNumber);
        _db.PhotoGears.Add(gear);
        await _db.SaveChangesAsync();
        return gear;
    }

    public async Task<IReadOnlyList<PhotoGear>> GetAllAsync()
    {
        return await _db.PhotoGears.ToListAsync();
    }

    public async Task<PhotoGear?> GetByIdAsync(Guid id)
    {
        return await _db.PhotoGears.FindAsync(id);
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        var gear = await _db.PhotoGears.FindAsync(id);
        if (gear is null) return false;
        _db.PhotoGears.Remove(gear);
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}
