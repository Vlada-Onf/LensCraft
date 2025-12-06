namespace Domain.Models;

public class PortfolioItem
{
    public Guid Id { get; private set; }
    public Guid PhotographerId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string ImageUrl { get; private set; } = string.Empty;
    public string? Location { get; private set; }
    public DateTime PublishedAt { get; private set; }
    protected PortfolioItem() { }

    private PortfolioItem(
        Guid id,
        Guid photographerId,
        string title,
        string description,
        string imageUrl,
        string? location,
        DateTime createdAt)
    {
        Id = id;
        PhotographerId = photographerId;
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        Location = location;
        PublishedAt = createdAt;
    }

    public static PortfolioItem Create(
        Guid id,
        Guid photographerId,
        string title,
        string description,
        string imageUrl,
        string? location)
    {
        return new PortfolioItem(id, photographerId, title, description, imageUrl, location, DateTime.UtcNow);
    }

    public void UpdateDetails(string title, string description, string imageUrl, string? location)
    {
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        Location = location;
    }
}
