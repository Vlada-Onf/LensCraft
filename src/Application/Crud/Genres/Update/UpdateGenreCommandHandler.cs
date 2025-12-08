using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Genres.Update
{
    public class UpdateGenreCommandHandler(IGenreService genreService)
     : IRequestHandler<UpdateGenreCommand, Result<UpdateGenreError, Genre>>
    {
        public async Task<Result<UpdateGenreError, Genre>> Handle(
    UpdateGenreCommand request,
    CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<UpdateGenreError, Genre>.Fail(UpdateGenreError.InvalidId);

            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<UpdateGenreError, Genre>.Fail(UpdateGenreError.InvalidName);

            var genre = await genreService.GetByIdAsync(request.Id);
            if (genre is null)
                return Result<UpdateGenreError, Genre>.Fail(UpdateGenreError.NotFound);

            genre.Rename(request.Name); 
            await genreService.SaveAsync();

            return Result<UpdateGenreError, Genre>.Success(genre);
        }
    }
}
