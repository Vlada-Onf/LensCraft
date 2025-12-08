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
    public enum GetGenreByIdError
    {
        InvalidId,
        NotFound
    }
    public record GetGenreById(Guid Id) : IRequest<Result<GetGenreByIdError, Genre>>;
}
