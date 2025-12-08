using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotoGears.Delete
{
    public enum DeletePhotoGearError
    {
        InvalidId,
        NotFound
    }
    public record DeletePhotoGearCommand(Guid Id) : IRequest<Result<DeletePhotoGearError, bool>>;
}
