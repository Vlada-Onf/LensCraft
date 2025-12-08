using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Photographers.Read
{
    public enum GetPhotographerByIdError
    {
        InvalidId,
        NotFound
    }

    public record GetPhotographerById(Guid Id)
        : IRequest<Result<GetPhotographerByIdError, Photographer>>;
}
