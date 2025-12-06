using Domain.Models;

namespace Api.Dtos;

public record PhotoGearDto(Guid Id, string Name, string Type, string Model, string SerialNumber, GearStatus Status, Guid PhotographerId)
{
    public static PhotoGearDto FromDomainModel(PhotoGear gear)
        => new(gear.Id, gear.Name, gear.Type, gear.Model, gear.SerialNumber, gear.Status, gear.PhotographerId);
}
public record CreatePhotoGearDto(string Name, string Type, string Model, string SerialNumber, Guid PhotographerId);
public record UpdatePhotoGearDto(string Name, string Model, GearStatus Status);