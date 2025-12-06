using Domain.Models;
using PhotographerModel = Domain.Models.Photographer;

namespace Test.Tests.Data.Photographer;

public static class PhotographerData
{
    public static PhotographerModel FirstPhotographer()
    {
        return PhotographerModel.Create(
            Guid.NewGuid(),
            "Lina",
            "Sharp",
            "lina@lens.com",
            "Fashion and editorial photographer"
        );
    }

    public static PhotographerModel SecondPhotographer()
    {
        return PhotographerModel.Create(
            Guid.NewGuid(),
            "Mark",
            "Stone",
            "mark@lens.com",
            "Landscape and wildlife photographer"
        );
    }
}
