using Domain.Models;

namespace Application.Common.Interfaces;

public interface IUserService
{
    Task<User> RegisterAsync(Guid id, string firstName, string lastName, string email);
    Task<User?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<User>> GetAllAsync();
    Task<bool> RemoveAsync(Guid id);
    Task SaveAsync();
}

