using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Crud.Comments.Read
{
    public class GetAllCommentsHandler(
    ICommentService commentService
) : IRequestHandler<GetAllComments, IReadOnlyList<Comment>>
    {
        public async Task<IReadOnlyList<Comment>> Handle(GetAllComments request, CancellationToken cancellationToken)
        {
            return await commentService.GetAllAsync();
        }
    }
}
