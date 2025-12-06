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
    public class GetCommentByIdQueryHandler(
        ICommentService commentService
    ) : IRequestHandler<GetCommentById, Comment?>
    {
        public async Task<Comment?> Handle(GetCommentById request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Comment ID must not be empty");

            return await commentService.GetByIdAsync(request.Id);
        }
    }
}
