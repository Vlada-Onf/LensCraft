using Domain.Models;

namespace Tests.Data.PortfolioItem;

public static class PortfolioItemData
{
    public static PortfolioItem FirstPortfolioItem(Guid photographerId) =>
        PortfolioItem.Create(
            Guid.NewGuid(),
            photographerId,
            "Sunset in Rivne",
            "Golden hour portrait",
            "https://example.com/sunset.jpg",
            "Rivne, Ukraine",
            DateTime.UtcNow
        );

    public static PortfolioItem SecondPortfolioItem(Guid photographerId) =>
        PortfolioItem.Create(
            Guid.NewGuid(),
            photographerId,
            "Urban Shadows",
            "Street photography in Kyiv",
            "https://example.com/urban.jpg",
            "Kyiv, Ukraine",
            DateTime.UtcNow
        );
}
