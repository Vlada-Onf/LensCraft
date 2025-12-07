using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration.EntityConfigurations
{
    public class UserFavoritePhotographerConfiguration
    : IEntityTypeConfiguration<UserFavoritePhotographer>
    {
        public void Configure(EntityTypeBuilder<UserFavoritePhotographer> builder)
        {
            builder.HasKey(x => new { x.UserId, x.PhotographerId });

            builder
                .HasOne(x => x.User)
                .WithMany(u => u.FavoritePhotographers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Photographer)
                .WithMany(p => p.FavoritedByUsers)
                .HasForeignKey(x => x.PhotographerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
