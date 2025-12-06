using System;
using Domain.Models;
using MediatR;

namespace Application.Crud.PhotoGears.Create;

public record AddPhotoGearCommand : IRequest<PhotoGear>
{
    public required Guid PhotographerId { get; init; }
    public required string Name { get; init; }
    public required string Type { get; init; }
    public required string Model { get; init; }
    public required string SerialNumber { get; init; }
}