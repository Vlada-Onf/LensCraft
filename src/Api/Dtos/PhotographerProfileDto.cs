using Domain.Models;

namespace Api.Dtos
{
    public record PhotographerProfileDto(
        Guid PhotographerId,
        string Phone,
        string Website,
        string Instagram)
    {
        public static PhotographerProfileDto FromDomainModel(PhotographerProfile profile)
            => new(profile.PhotographerId, profile.Phone, profile.Website, profile.Instagram);
    }

    public record CreatePhotographerProfileDto(
        string Phone,
        string Website,
        string Instagram);

    public record UpdatePhotographerProfileDto(
        string Phone,
        string Website,
        string Instagram);
}
