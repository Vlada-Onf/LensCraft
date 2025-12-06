using CommentModel = Domain.Models.Comment;

namespace Test.Tests.Data.Comment;

public static class CommentData
{
    public static CommentModel FirstTestComment(Guid portfolioItemId, Guid authorId)
    {
        return CommentModel.Create(
            Guid.NewGuid(),
            portfolioItemId,
            authorId,
            "This is the first test comment."
        );
    }

    public static CommentModel SecondTestComment(Guid portfolioItemId, Guid authorId)
    {
        return CommentModel.Create(
            Guid.NewGuid(),
            portfolioItemId,
            authorId,
            "This is the second test comment."
        );
    }
}
