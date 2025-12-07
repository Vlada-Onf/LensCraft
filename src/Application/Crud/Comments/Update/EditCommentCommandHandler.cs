using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;

namespace Application.Crud.Comments.Update;

public class EditCommentCommandHandler
    : IRequestHandler<EditCommentCommand, Result<EditCommentError, Comment>>
{
    private readonly ICommentService _commentService;

    public EditCommentCommandHandler(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<Result<EditCommentError, Comment>> Handle(
        EditCommentCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            return Result<EditCommentError, Comment>.Fail(EditCommentError.InvalidId);
        }
        if (string.IsNullOrWhiteSpace(request.NewText))
        {
            return Result<EditCommentError, Comment>.Fail(EditCommentError.InvalidText);
        }
        var comment = await _commentService.GetByIdAsync(request.Id);
        if (comment is null)
        {
            return Result<EditCommentError, Comment>.Fail(EditCommentError.NotFound);
        }
        comment.Edit(request.NewText);
        await _commentService.SaveAsync();
        return Result<EditCommentError, Comment>.Success(comment);
    }
}
