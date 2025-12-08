using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Genres.Read
{
    public class GetAllGenresHandler(IGenreService genreService)
    : IRequestHandler<GetAllGenres, IReadOnlyList<Genre>>
    {
        public async Task<IReadOnlyList<Genre>> Handle(
            GetAllGenres request,
            CancellationToken cancellationToken)
        {
            return await genreService.GetAllAsync();
        }
    }
}
