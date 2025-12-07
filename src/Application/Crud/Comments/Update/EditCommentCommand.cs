using Application.Common.Results;
using Domain.Models;
using MediatR;

namespace Application.Crud.Comments.Update;

public record EditCommentCommand
    : IRequest<Result<EditCommentError, Comment>>
{
    public required Guid Id { get; init; }
    public required string NewText { get; init; }
}

public enum EditCommentError
{
    InvalidId,
    InvalidText,
    NotFound
}