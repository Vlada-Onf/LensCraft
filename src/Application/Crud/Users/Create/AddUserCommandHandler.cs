using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Users.Create
{
    public class AddUserCommandHandler(IUserService userService)
    : IRequestHandler<AddUserCommand, Result<AddUserError, User>>
    {
        public async Task<Result<AddUserError, User>> Handle(
            AddUserCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
                return Result<AddUserError, User>.Fail(AddUserError.InvalidFirstName);

            if (string.IsNullOrWhiteSpace(request.LastName))
                return Result<AddUserError, User>.Fail(AddUserError.InvalidLastName);

            if (string.IsNullOrWhiteSpace(request.Email))
                return Result<AddUserError, User>.Fail(AddUserError.InvalidEmail);

            var user = await userService.RegisterAsync(
                Guid.NewGuid(),
                request.FirstName,
                request.LastName,
                request.Email);

            return Result<AddUserError, User>.Success(user);
        }
    }
}
