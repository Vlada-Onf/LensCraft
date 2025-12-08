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
    public enum UpdateGenreError
    {
        InvalidId,
        InvalidName,
        NotFound
    }
    public record UpdateGenreCommand : IRequest<Result<UpdateGenreError, Genre>>
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
    }
}
