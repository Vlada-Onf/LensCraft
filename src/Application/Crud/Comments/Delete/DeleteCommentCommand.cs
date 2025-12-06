using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Comments.Delete
{
    public record DeleteCommentCommand(Guid Id) : IRequest<bool>;
}
