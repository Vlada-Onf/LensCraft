using MediatR;
using Domain.Models;

namespace Application.Crud.Comments.Update;

public record EditCommentCommand : IRequest<Comment>
{
    public required Guid Id { get; init; }
    public required string NewText { get; init; }
}
