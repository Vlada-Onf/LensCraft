using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.EntityConfigurations;

public class PortfolioItemConfiguration : IEntityTypeConfiguration<PortfolioItem>
{
    public void Configure(EntityTypeBuilder<PortfolioItem> builder)
    {
        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.Title).IsRequired().HasMaxLength(200);
        builder.Property(pi => pi.Description).IsRequired().HasMaxLength(1000);
        builder.Property(pi => pi.ImageUrl).IsRequired();
        builder.Property(pi => pi.Location).HasMaxLength(200);
        builder.Property(pi => pi.PublishedAt).IsRequired();

        builder.HasOne<Photographer>()
            .WithMany()
            .HasForeignKey(pi => pi.PhotographerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
