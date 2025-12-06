using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Users.Read
{
    public class GetUserByIdQueryHandler(
    IUserService userService
) : IRequestHandler<GetUserByIdQuery, User?>
    {
        public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Id must not be empty");

            return await userService.GetByIdAsync(request.Id);
        }
    }

}
