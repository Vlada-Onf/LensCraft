using Application.Common.Interfaces;
using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Users.Delete
{
    public class DeleteUserCommandHandler(IUserService userService)
    : IRequestHandler<DeleteUserCommand, Result<DeleteUserError, bool>>
    {
        public async Task<Result<DeleteUserError, bool>> Handle(
            DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<DeleteUserError, bool>.Fail(DeleteUserError.InvalidId);

            var deleted = await userService.RemoveAsync(request.Id);
            if (!deleted)
                return Result<DeleteUserError, bool>.Fail(DeleteUserError.NotFound);

            return Result<DeleteUserError, bool>.Success(true);
        }
    }
}
