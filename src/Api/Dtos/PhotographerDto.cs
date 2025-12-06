using Domain.Models;

namespace Api.Dtos;

public record PhotographerDto(Guid Id, string FirstName, string LastName, string Email, string Bio)
{
    public static PhotographerDto FromDomainModel(Photographer photographer)
        => new(photographer.Id, photographer.FirstName, photographer.LastName, photographer.Email, photographer.Bio);
}

public record CreatePhotographerDto(string FirstName, string LastName, string Email, string Bio);
public record UpdatePhotographerDto(string FirstName, string LastName, string Bio);