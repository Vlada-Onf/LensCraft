using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Genres.Read
{
    public class GetGenreByIdHandler(IGenreService genreService)
    : IRequestHandler<GetGenreById, Result<GetGenreByIdError, Genre>>
    {
        public async Task<Result<GetGenreByIdError, Genre>> Handle(
            GetGenreById request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<GetGenreByIdError, Genre>.Fail(GetGenreByIdError.InvalidId);

            var genre = await genreService.GetByIdAsync(request.Id);
            if (genre is null)
                return Result<GetGenreByIdError, Genre>.Fail(GetGenreByIdError.NotFound);

            return Result<GetGenreByIdError, Genre>.Success(genre);
        }
    }
}
