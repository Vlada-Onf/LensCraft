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
    public class PhotographerProfileConfiguration : IEntityTypeConfiguration<PhotographerProfile>
    {
        public void Configure(EntityTypeBuilder<PhotographerProfile> builder)
        {
            builder.HasKey(pp => pp.PhotographerId);

            builder.Property(pp => pp.Phone)
                .HasMaxLength(50);

            builder.Property(pp => pp.Website)
                .HasMaxLength(200);

            builder.Property(pp => pp.Instagram)
                .HasMaxLength(200);

            builder
                .HasOne(pp => pp.Photographer)
                .WithOne(p => p.Profile)
                .HasForeignKey<PhotographerProfile>(pp => pp.PhotographerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
