using Domain.Models;

namespace Tests.Data.Comment;

public static class CommentData
{
    public static Comment FirstComment(Guid portfolioItemId, Guid authorId) =>
        Comment.Create(
            Guid.NewGuid(),
            portfolioItemId,
            authorId,
            "Amazing shot!",
            DateTime.UtcNow
        );

    public static Comment SecondComment(Guid portfolioItemId, Guid authorId) =>
        Comment.Create(
            Guid.NewGuid(),
            portfolioItemId,
            authorId,
            "Love the composition!",
            DateTime.UtcNow
        );
}
