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
    public class GetAllCommentsHandler(
    ICommentService commentService
) : IRequestHandler<GetAllComments, Result<GetAllCommentsError, IReadOnlyList<Comment>>>
    {
        public async Task<Result<GetAllCommentsError, IReadOnlyList<Comment>>> Handle(
            GetAllComments request,
            CancellationToken cancellationToken)
        {
            var comments = await commentService.GetAllAsync();
            return Result<GetAllCommentsError, IReadOnlyList<Comment>>.Success(comments);
        }
    }
}
