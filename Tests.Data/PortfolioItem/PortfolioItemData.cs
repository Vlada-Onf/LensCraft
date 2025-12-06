using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioItemModel = Domain.Models.PortfolioItem;

namespace Test.Tests.Data.PortfolioItem
{
    public static class PortfolioItemData
    {
        public static PortfolioItemModel FirstPortfolioItem(Guid photographerId)
        {
            return PortfolioItemModel.Create(
                Guid.NewGuid(),
                photographerId,
                "Golden Hour Portrait",
                "A stunning portrait taken during sunset",
                "https://lenscraft.com/images/golden-hour.jpg",
                "Kyiv, Ukraine"
            );
        }

        public static PortfolioItemModel SecondPortfolioItem(Guid photographerId)
        {
            return PortfolioItemModel.Create(
                Guid.NewGuid(),
                photographerId,
                "Urban Shadows",
                "Black and white street photography in downtown",
                "https://lenscraft.com/images/urban-shadows.jpg",
                "Lviv, Ukraine"
            );
        }
    }
}
