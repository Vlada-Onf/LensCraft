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
    public record GetCommentById(Guid Id)
    : IRequest<Result<GetCommentByIdError, Comment>>;

    public enum GetCommentByIdError
    {
        InvalidId,
        NotFound
    }
}
