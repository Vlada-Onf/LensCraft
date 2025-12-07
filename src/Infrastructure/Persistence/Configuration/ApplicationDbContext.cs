using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Configuration;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Photographer> Photographers => Set<Photographer>();
    public DbSet<PortfolioItem> PortfolioItems => Set<PortfolioItem>();
    public DbSet<PhotoGear> PhotoGears => Set<PhotoGear>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<PhotographerGenre> PhotographerGenres => Set<PhotographerGenre>();
    public DbSet<UserFavoritePhotographer> UserFavoritePhotographers => Set<UserFavoritePhotographer>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
