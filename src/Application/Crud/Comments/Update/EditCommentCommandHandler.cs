using Application.Common.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Crud.Comments.Update;

public class EditCommentCommandHandler : IRequestHandler<EditCommentCommand, Comment>
{
    private readonly ICommentService _commentService;

    public EditCommentCommandHandler(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<Comment> Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        // ПРОСТА ВАЛІДАЦІЯ
        if (string.IsNullOrWhiteSpace(request.NewText))
        {
            throw new ArgumentException("Text is required");
        }

        var comment = await _commentService.GetByIdAsync(request.Id);
        if (comment is null)
            throw new KeyNotFoundException($"Comment with ID {request.Id} not found");

        comment.Edit(request.NewText);
        await _commentService.SaveAsync();

        return comment;
    }
}
