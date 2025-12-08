using Domain.Models;

namespace Api.Dtos
{
    public record GenreDto(Guid Id, string Name)
    {
        public static GenreDto FromDomainModel(Genre genre)
            => new(genre.Id, genre.Name);
    }
    public record CreateGenreDto(string Name);

    public record UpdateGenreDto(string Name);
}
