using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;

namespace Application.Crud.PhotoGears.Create;

public enum AddPhotoGearError
{
    InvalidPhotographerId,
    InvalidName,
    InvalidType,
    InvalidModel,
    InvalidSerialNumber
}

public record AddPhotoGearCommand : IRequest<Result<AddPhotoGearError, PhotoGear>>
{
    public required Guid PhotographerId { get; init; }
    public required string Name { get; init; }
    public required string Type { get; init; }
    public required string Model { get; init; }
    public required string SerialNumber { get; init; }
}