using Application.Common.Interfaces;
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
    public class GetUserByIdQueryHandler(IUserService userService)
    : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdError, User>>
    {
        public async Task<Result<GetUserByIdError, User>> Handle(
            GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<GetUserByIdError, User>.Fail(GetUserByIdError.InvalidId);

            var user = await userService.GetByIdAsync(request.Id);
            if (user is null)
                return Result<GetUserByIdError, User>.Fail(GetUserByIdError.NotFound);

            return Result<GetUserByIdError, User>.Success(user);
        }
    }
}
