using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;

namespace Application.Crud.Comments.Create;

public class AddCommentCommandHandler(
    ICommentService commentService
) : IRequestHandler<AddCommentCommand, Result<AddCommentError, Comment>>
{
    public async Task<Result<AddCommentError, Comment>> Handle(
        AddCommentCommand request,
        CancellationToken cancellationToken)
    {
        if (request.PortfolioItemId == Guid.Empty)
        {
            return Result<AddCommentError, Comment>.Fail(
                AddCommentError.InvalidPortfolioItemId);
        }
        if (request.AuthorId == Guid.Empty)
        {
            return Result<AddCommentError, Comment>.Fail(AddCommentError.InvalidAuthorId);
        }
        var comment = await commentService.CreateAsync(
            Guid.NewGuid(),
            request.PortfolioItemId,
            request.AuthorId,
            request.Text
        );
        return Result<AddCommentError, Comment>.Success(comment);
    }
}
