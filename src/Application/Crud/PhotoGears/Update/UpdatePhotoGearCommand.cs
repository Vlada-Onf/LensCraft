using Application.Common.Results;
using Domain.Models;
using MediatR;

namespace Application.Crud.PhotoGears.Update;

public enum UpdatePhotoGearError
{
    InvalidId,
    InvalidName,
    InvalidModel,
    NotFound
}

public record UpdatePhotoGearCommand : IRequest<Result<UpdatePhotoGearError, PhotoGear>>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Model { get; init; }
    public required GearStatus Status { get; init; }
}
