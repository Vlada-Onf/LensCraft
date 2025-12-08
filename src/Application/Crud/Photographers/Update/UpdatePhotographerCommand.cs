using Application.Common.Results;
using Domain.Models;
using MediatR;

namespace Application.Crud.Photographers.Update;

public enum UpdatePhotographerError
{
    InvalidId,
    InvalidFirstName,
    InvalidLastName,
    NotFound
}

public record UpdatePhotographerCommand
    : IRequest<Result<UpdatePhotographerError, Photographer>>
{
    public Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Bio { get; init; }
}