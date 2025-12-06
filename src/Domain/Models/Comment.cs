namespace Domain.Models;
public class Comment
{
    public Guid Id { get; private set; }
    public Guid PortfolioItemId { get; private set; }
    public Guid AuthorId { get; private set; }
    public string Text { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    protected Comment() { }

    private Comment(Guid id, Guid portfolioItemId, Guid authorId, string text, DateTime createdAt)
    {
        Id = id;
        PortfolioItemId = portfolioItemId;
        AuthorId = authorId;
        Text = text;
        CreatedAt = createdAt;
    }

    public static Comment Create(Guid id, Guid portfolioItemId, Guid authorId, string text)
    {
        return new Comment(id, portfolioItemId, authorId, text, DateTime.UtcNow);
    }

    public void Edit(string newText)
    {
        Text = newText;
    }
}
