using MediatR;
using Application.Common.Interfaces;
using Domain.Models;

namespace Application.Crud.Comments.Create;

public class AddCommentCommandHandler(
    ICommentService commentService
) : IRequestHandler<AddCommentCommand, Comment>
{
    public async Task<Comment> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        if (request.PortfolioItemId == Guid.Empty)
            throw new ArgumentException("PortfolioItemId must not be empty");

        if (request.AuthorId == Guid.Empty)
            throw new ArgumentException("AuthorId must not be empty");

        var comment = await commentService.CreateAsync(
            Guid.NewGuid(),
            request.PortfolioItemId,
            request.AuthorId,
            request.Text
        );

        return comment;
    }
}
