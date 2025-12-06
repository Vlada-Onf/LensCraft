using Domain.Models;

namespace Api.Dtos;

public record CommentDto(Guid Id, Guid PortfolioItemId, Guid AuthorId, string Text, DateTime CreatedAt)
{
    public static CommentDto FromDomainModel(Comment comment)
        => new(comment.Id, comment.PortfolioItemId, comment.AuthorId, comment.Text, comment.CreatedAt);
}

public record CreateCommentDto(Guid PortfolioItemId, Guid AuthorId, string Text);
public record EditCommentDto(string Text);