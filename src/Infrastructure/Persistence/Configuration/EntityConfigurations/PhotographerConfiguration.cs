using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.EntityConfigurations;

public class PhotographerConfiguration : IEntityTypeConfiguration<Photographer>
{
    public void Configure(EntityTypeBuilder<Photographer> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Email).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Bio).IsRequired().HasMaxLength(1000);
        builder.Property(p => p.RegisteredAt).IsRequired();
    }
}
