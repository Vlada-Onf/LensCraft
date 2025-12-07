namespace Domain.Models;
public class Photographer
{
    public Guid Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Bio { get; private set; }
    public DateTime RegisteredAt { get; }
    public ICollection<PhotographerGenre> Genres { get; }
    public ICollection<UserFavoritePhotographer> FavoritedByUsers { get; }
    public PhotographerProfile? Profile { get; private set; }
    private Photographer(Guid id, string firstName, string lastName, string email, string bio, DateTime registeredAt)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Bio = bio;
        RegisteredAt = registeredAt;
        Genres = new List<PhotographerGenre>();
        FavoritedByUsers = new List<UserFavoritePhotographer>();
    }

    public static Photographer Create(Guid id, string firstName, string lastName, string email, string bio)
    {
        return new Photographer(id, firstName, lastName, email, bio, DateTime.UtcNow);
    }

    public void UpdateDetails(string firstName, string lastName, string bio)
    {
        FirstName = firstName;
        LastName = lastName;
        Bio = bio;
    }
    public void SetProfile(PhotographerProfile profile)
    {
        Profile = profile;
    }
}
