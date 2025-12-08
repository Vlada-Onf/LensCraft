using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPhotographerProfileService
    {
        Task<PhotographerProfile?> GetByPhotographerIdAsync(Guid photographerId);
        Task<PhotographerProfile> CreateAsync(Guid photographerId, string phone, string website, string instagram);
        Task SaveAsync();
    }
}
