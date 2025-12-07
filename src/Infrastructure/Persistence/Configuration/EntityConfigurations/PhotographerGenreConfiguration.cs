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
    public class PhotographerGenreConfiguration : IEntityTypeConfiguration<PhotographerGenre>
    {
        public void Configure(EntityTypeBuilder<PhotographerGenre> builder)
        {
            builder.HasKey(pg => new { pg.PhotographerId, pg.GenreId });

            builder
                .HasOne(pg => pg.Photographer)
                .WithMany(p => p.Genres)
                .HasForeignKey(pg => pg.PhotographerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(pg => pg.Genre)
                .WithMany(g => g.Photographers)
                .HasForeignKey(pg => pg.GenreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
