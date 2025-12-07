using Application.Common.Interfaces;
using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Comments.Delete
{
    public class DeleteCommentCommandHandler(
    ICommentService commentService
) : IRequestHandler<DeleteCommentCommand, Result<DeleteCommentError, bool>>
    {
        public async Task<Result<DeleteCommentError, bool>> Handle(
        DeleteCommentCommand request,
        CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<DeleteCommentError, bool>.Fail(DeleteCommentError.InvalidId);

            var deleted = await commentService.RemoveAsync(request.Id);

            if (!deleted)
                return Result<DeleteCommentError, bool>.Fail(DeleteCommentError.NotFound);

            return Result<DeleteCommentError, bool>.Success(true);
        }
    }
}
