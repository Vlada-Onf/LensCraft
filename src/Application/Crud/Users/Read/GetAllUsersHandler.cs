using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Crud.Users.Read
{
    public class GetAllUsersHandler(IUserService userService)
        : IRequestHandler<GetAllUsers, IReadOnlyList<User>>
    {
        public async Task<IReadOnlyList<User>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            return await userService.GetAllAsync();
        }
    }
}
