using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.EntityConfigurations;

public class PhotoGearConfiguration : IEntityTypeConfiguration<PhotoGear>
{
    public void Configure(EntityTypeBuilder<PhotoGear> builder)
    {
        builder.HasKey(pg => pg.Id);

        builder.Property(pg => pg.Name).IsRequired().HasMaxLength(100);
        builder.Property(pg => pg.Type).IsRequired().HasMaxLength(50);
        builder.Property(pg => pg.Model).IsRequired().HasMaxLength(100);
        builder.Property(pg => pg.SerialNumber).IsRequired().HasMaxLength(100);
        builder.Property(pg => pg.Status).IsRequired();
        builder.Property(pg => pg.CreatedAt).IsRequired();

        builder.HasOne<Photographer>()
            .WithMany()
            .HasForeignKey(pg => pg.PhotographerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
