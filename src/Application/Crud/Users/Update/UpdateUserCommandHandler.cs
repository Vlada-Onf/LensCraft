using Application.Common.Interfaces;
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
    public class UpdateUserCommandHandler(IUserService userService)
    : IRequestHandler<UpdateUserCommand, Result<UpdateUserError, User>>
    {
        public async Task<Result<UpdateUserError, User>> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<UpdateUserError, User>.Fail(UpdateUserError.InvalidId);

            if (string.IsNullOrWhiteSpace(request.FirstName))
                return Result<UpdateUserError, User>.Fail(UpdateUserError.InvalidFirstName);

            if (string.IsNullOrWhiteSpace(request.LastName))
                return Result<UpdateUserError, User>.Fail(UpdateUserError.InvalidLastName);

            var user = await userService.GetByIdAsync(request.Id);
            if (user is null)
                return Result<UpdateUserError, User>.Fail(UpdateUserError.NotFound);

            user.UpdateName(request.FirstName, request.LastName);
            await userService.SaveAsync();

            return Result<UpdateUserError, User>.Success(user);
        }
    }
}