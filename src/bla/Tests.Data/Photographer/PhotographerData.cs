using Domain.Models;

namespace Tests.Data.Photographer;

public static class PhotographerData
{
    public static Photographer FirstPhotographer() =>
        Photographer.Create(
            Guid.NewGuid(),
            "Anna",
            "Lens",
            "anna@lens.com",
            "Portrait specialist"
        );

    public static Photographer SecondPhotographer() =>
        Photographer.Create(
            Guid.NewGuid(),
            "Mark",
            "Craft",
            "mark@craft.com",
            "Landscape lover"
        );
}
