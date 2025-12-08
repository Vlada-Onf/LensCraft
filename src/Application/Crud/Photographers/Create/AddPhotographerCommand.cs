using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Photographers.Create
{
    public enum AddPhotographerError
    {
        InvalidFirstName,
        InvalidLastName,
        InvalidEmail
    }

    public record AddPhotographerCommand
        : IRequest<Result<AddPhotographerError, Photographer>>
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public required string Bio { get; init; }
    }
}
