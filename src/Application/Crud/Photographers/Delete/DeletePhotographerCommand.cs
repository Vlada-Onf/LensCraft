using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Photographers.Delete
{
    public enum DeletePhotographerError
    {
        InvalidId,
        NotFound
    }

    public record DeletePhotographerCommand(Guid Id)
        : IRequest<Result<DeletePhotographerError, bool>>;
}
