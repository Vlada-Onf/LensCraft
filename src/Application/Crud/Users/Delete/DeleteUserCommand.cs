using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Users.Delete
{
    public enum DeleteUserError
    {
        InvalidId,
        NotFound
    }

    public record DeleteUserCommand(Guid Id) : IRequest<Result<DeleteUserError, bool>>;
}
