using Domain.Models;

namespace Application.Common.Interfaces;

public interface ICommentService
{
    Task<Comment> CreateAsync(Guid id, Guid portfolioItemId, Guid authorId, string text);
    Task<Comment?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Comment>> GetAllAsync();
    Task<bool> RemoveAsync(Guid id);
    Task SaveAsync();
}
