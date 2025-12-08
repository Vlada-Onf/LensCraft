using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Users.Update
{
    public enum UpdateUserError
    {
        InvalidId,
        InvalidFirstName,
        InvalidLastName,
        NotFound
    }

    public record UpdateUserCommand : IRequest<Result<UpdateUserError, User>>
    {
        public Guid Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
    }
}
