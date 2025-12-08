using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Genres.Create
{
    public class AddGenreCommandHandler(IGenreService genreService)
    : IRequestHandler<AddGenreCommand, Result<AddGenreError, Genre>>
    {
        public async Task<Result<AddGenreError, Genre>> Handle(
            AddGenreCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<AddGenreError, Genre>.Fail(AddGenreError.InvalidName);

            var genre = await genreService.CreateAsync(Guid.NewGuid(), request.Name);
            return Result<AddGenreError, Genre>.Success(genre);
        }
    }
}
