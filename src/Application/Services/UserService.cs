using Application.Common.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _db;

    public UserService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<User> RegisterAsync(Guid id, string firstName, string lastName, string email)
    {
        var user = User.Register(id, firstName, lastName, email);
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _db.Users.FindAsync(id);
    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return false;
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}
