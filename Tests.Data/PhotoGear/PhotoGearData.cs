using PhotoGearModel = Domain.Models.PhotoGear;

namespace Test.Tests.Data.PhotoGear;

public static class PhotoGearData
{
    public static PhotoGearModel FirstTestGear(Guid photographerId)
    {
        return PhotoGearModel.New(
            Guid.NewGuid(),
            photographerId,
            "Canon EOS R5",
            "Camera",
            "EOSR5",
            "SN123456"
        );
    }

    public static PhotoGearModel SecondTestGear(Guid photographerId)
    {
        return PhotoGearModel.New(
            Guid.NewGuid(),
            photographerId,
            "Sigma 35mm f/1.4",
            "Lens",
            "SIGMA35",
            "SN654321"
        );
    }
}
