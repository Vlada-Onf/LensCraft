using Domain.Models;

namespace Application.Common.Interfaces;

public interface IPhotographerService
{
    Task<Photographer> RegisterAsync(Guid id, string firstName, string lastName, string email, string bio);
    Task<Photographer?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Photographer>> GetAllAsync();
    Task<bool> RemoveAsync(Guid id);
    Task SaveAsync();
}


