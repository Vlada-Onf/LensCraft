using Application.Common.Interfaces;
using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Genres.Delete
{
    public class DeleteGenreCommandHandler(IGenreService genreService)
    : IRequestHandler<DeleteGenreCommand, Result<DeleteGenreError, bool>>
    {
        public async Task<Result<DeleteGenreError, bool>> Handle(
            DeleteGenreCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<DeleteGenreError, bool>.Fail(DeleteGenreError.InvalidId);

            var deleted = await genreService.DeleteAsync(request.Id);
            if (!deleted)
                return Result<DeleteGenreError, bool>.Fail(DeleteGenreError.NotFound);

            return Result<DeleteGenreError, bool>.Success(true);
        }
    }
}
