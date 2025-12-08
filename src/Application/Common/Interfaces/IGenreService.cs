using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGenreService
    {
        Task<Genre?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Genre>> GetAllAsync();
        Task<Genre> CreateAsync(Guid id, string name);
        Task<bool> DeleteAsync(Guid id);
        Task SaveAsync();
    }
}
