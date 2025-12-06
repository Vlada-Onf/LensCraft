using Domain.Models;
using MediatR;

namespace Application.Crud.Photographers.Update;

public record UpdatePhotographerCommand : IRequest<Photographer>
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Bio { get; init; } = string.Empty;
}
