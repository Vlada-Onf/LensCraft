namespace Domain.Models;
public class User
{
    public Guid Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public DateTime RegisteredAt { get; }

    public ICollection<Comment> CommentsLeft { get; }

    private User(Guid id, string firstName, string lastName, string email, DateTime registeredAt)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        RegisteredAt = registeredAt;
        CommentsLeft = new List<Comment>();
    }

    public static User Register(Guid id, string firstName, string lastName, string email)
    {
        return new User(id, firstName, lastName, email, DateTime.UtcNow);
    }

    public void UpdateDetails(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void UpdateName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
