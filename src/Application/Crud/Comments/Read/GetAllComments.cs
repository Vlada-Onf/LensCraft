using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Comments.Read;

public record GetAllComments
    : IRequest<Result<GetAllCommentsError, IReadOnlyList<Comment>>>;

public enum GetAllCommentsError
{
    None
}