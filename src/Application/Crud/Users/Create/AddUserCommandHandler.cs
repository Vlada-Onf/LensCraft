using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Users.Create
{
    public class AddUserCommandHandler(
    IUserService userService
) : IRequestHandler<AddUserCommand, User>
    {
        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.RegisterAsync(
                Guid.NewGuid(),
                request.FirstName,
                request.LastName,
                request.Email
            );

            return user;
        }
    }
}
