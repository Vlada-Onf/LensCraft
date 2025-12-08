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
    public enum AddGenreError
    {
        InvalidName
    }
    public record AddGenreCommand : IRequest<Result<AddGenreError, Genre>>
    {
        public required string Name { get; init; }
    }
}
