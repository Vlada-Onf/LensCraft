using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Users.Update
{
    public class UpdateUserCommandHandler(
    IUserService userService
) : IRequestHandler<UpdateUserCommand, User>
    {
        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Id must not be empty");

            var user = await userService.GetByIdAsync(request.Id);
            if (user is null)
                throw new KeyNotFoundException($"User with ID {request.Id} not found");

            user.UpdateName(request.FirstName, request.LastName);
            await userService.SaveAsync();

            return user;
        }

    }
}