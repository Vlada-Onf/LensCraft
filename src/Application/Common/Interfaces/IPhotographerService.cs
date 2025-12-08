using Domain.Models;

namespace Application.Common.Interfaces;

public interface IPhotographerService
{
    Task<Photographer?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Photographer>> GetAllAsync();
    Task<Photographer> CreateAsync(Guid id, string firstName, string lastName, string email, string bio);
    Task<bool> DeleteAsync(Guid id);
    Task SaveChangesAsync();
}
