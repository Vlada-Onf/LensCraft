using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Genres.Delete
{
    public enum DeleteGenreError
    {
        InvalidId,
        NotFound
    }
    public record DeleteGenreCommand(Guid Id) : IRequest<Result<DeleteGenreError, bool>>;
}
