using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Users.Read
{
    public enum GetUserByIdError
    {
        InvalidId,
        NotFound
    }

    public record GetUserByIdQuery(Guid Id) : IRequest<Result<GetUserByIdError, User>>;
}
