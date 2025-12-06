using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.EntityConfigurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Text).IsRequired().HasMaxLength(1000);
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.PortfolioItemId).IsRequired();
        builder.Property(c => c.AuthorId).IsRequired();

        builder.HasOne<PortfolioItem>()
            .WithMany()
            .HasForeignKey(c => c.PortfolioItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>()
            .WithMany(u => u.CommentsLeft)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
