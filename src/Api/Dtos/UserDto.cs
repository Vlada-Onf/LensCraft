using Domain.Models;

namespace Api.Dtos;

public record UserDto(Guid Id, string FirstName, string LastName, string Email)
{
    public static UserDto FromDomainModel(User user)
        => new(user.Id, user.FirstName, user.LastName, user.Email);
}

public record CreateUserDto(string FirstName, string LastName, string Email);
public record UpdateUserDto(string FirstName, string LastName);
