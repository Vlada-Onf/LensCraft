using Domain.Models;

namespace Api.Dtos;

public record PortfolioItemDto(Guid Id, string Title, string Description, string ImageUrl, string? Location, DateTime PublishedAt, Guid PhotographerId
)
{
    public static PortfolioItemDto FromDomainModel(PortfolioItem item)
        => new(item.Id, item.Title, item.Description, item.ImageUrl, item.Location, item.PublishedAt, item.PhotographerId);
}
public record CreatePortfolioItemDto(
    string Title,
    string Description,
    string ImageUrl,
    string? Location,
    Guid PhotographerId
);
public record UpdatePortfolioItemDto(
    string Title,
    string Description,
    string ImageUrl,
    string? Location
);
