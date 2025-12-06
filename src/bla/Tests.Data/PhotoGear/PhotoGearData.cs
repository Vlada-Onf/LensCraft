using Domain.Models;

namespace Tests.Data.PhotoGear;

public static class PhotoGearData
{
    public static PhotoGear FirstGear(Guid photographerId) =>
        PhotoGear.Create(
            Guid.NewGuid(),
            "Canon EOS R5",
            "Camera",
            "R5-123456",
            "Ready",
            DateTime.UtcNow,
            photographerId
        );

    public static PhotoGear SecondGear(Guid photographerId) =>
        PhotoGear.Create(
            Guid.NewGuid(),
            "Sigma 35mm f/1.4",
            "Lens",
            "SIG-987654",
            "Calibrated",
            DateTime.UtcNow,
            photographerId
        );
}
