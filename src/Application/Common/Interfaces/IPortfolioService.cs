using Domain.Models;

namespace Application.Common.Interfaces;
public interface IPortfolioService
{
    Task<PortfolioItem> AddAsync(Guid id, Guid photographerId, string title, string description, string imageUrl, string? location);
    Task<IReadOnlyList<PortfolioItem>> GetAllAsync();
    Task<PortfolioItem?> GetByIdAsync(Guid id);
    Task<bool> RemoveAsync(Guid id);
    Task SaveAsync();
}
