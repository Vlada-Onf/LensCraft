using MediatR;
using Domain.Models;

namespace Application.Crud.PhotoGears.Update;

public record UpdatePhotoGearCommand : IRequest<PhotoGear>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Model { get; init; }
    public required GearStatus Status { get; init; }
}
