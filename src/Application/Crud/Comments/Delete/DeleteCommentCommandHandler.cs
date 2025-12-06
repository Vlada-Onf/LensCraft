using Application.Common.Interfaces;
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
    ) : IRequestHandler<DeleteCommentCommand, bool>
    {
        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Comment ID must not be empty");

            var success = await commentService.RemoveAsync(request.Id);
            return success;
        }
    }
}
