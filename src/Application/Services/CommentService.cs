using Application.Common.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly ApplicationDbContext _db;

    public CommentService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Comment> CreateAsync(Guid id, Guid portfolioItemId, Guid authorId, string text)
    {
        var portfolioExists = await _db.PortfolioItems.AnyAsync(p => p.Id == portfolioItemId);
        var userExists = await _db.Users.AnyAsync(u => u.Id == authorId);

        if (!portfolioExists || !userExists)
            throw new ArgumentException("PortfolioItem or User not found");

        var comment = Comment.Create(id, portfolioItemId, authorId, text);
        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        return await _db.Comments.FindAsync(id);
    }

    public async Task<IReadOnlyList<Comment>> GetAllAsync()
    {
        return await _db.Comments.ToListAsync();
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        var comment = await _db.Comments.FindAsync(id);
        if (comment is null) return false;
        _db.Comments.Remove(comment);
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}
