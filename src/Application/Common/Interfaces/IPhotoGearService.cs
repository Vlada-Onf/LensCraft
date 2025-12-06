using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPhotoGearService
    {
        Task<PhotoGear> AddAsync(Guid id, Guid photographerId, string name, string type, string model, string serialNumber);
        Task<IReadOnlyList<PhotoGear>> GetAllAsync();
        Task<PhotoGear?> GetByIdAsync(Guid id);
        Task<bool> RemoveAsync(Guid id);
        Task SaveAsync();
    }
}
