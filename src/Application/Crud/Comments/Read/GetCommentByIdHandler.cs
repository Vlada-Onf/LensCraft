using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Comments.Read
{
    public class GetCommentByIdQueryHandler(
    ICommentService commentService
) : IRequestHandler<GetCommentById, Result<GetCommentByIdError, Comment>>
    {
        public async Task<Result<GetCommentByIdError, Comment>> Handle(
            GetCommentById request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<GetCommentByIdError, Comment>.Fail(GetCommentByIdError.InvalidId);

            var comment = await commentService.GetByIdAsync(request.Id);

            if (comment is null)
                return Result<GetCommentByIdError, Comment>.Fail(GetCommentByIdError.NotFound);

            return Result<GetCommentByIdError, Comment>.Success(comment);
        }
    }
}
