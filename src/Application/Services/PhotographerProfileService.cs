using Application.Common.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PhotographerProfileService : IPhotographerProfileService
    {
        private readonly ApplicationDbContext _db;

        public PhotographerProfileService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<PhotographerProfile?> GetByPhotographerIdAsync(Guid photographerId)
        {
            return await _db.PhotographerProfiles
                .FirstOrDefaultAsync(p => p.PhotographerId == photographerId);
        }

        public async Task<PhotographerProfile> CreateAsync(
            Guid photographerId,
            string phone,
            string website,
            string instagram)
        {
            var profile = PhotographerProfile.Create(photographerId, phone, website, instagram);
            _db.PhotographerProfiles.Add(profile);
            await _db.SaveChangesAsync();
            return profile;
        }

        public Task SaveAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
