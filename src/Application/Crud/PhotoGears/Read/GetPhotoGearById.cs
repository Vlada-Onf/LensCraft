using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotoGears.Read
{
    public enum GetPhotoGearByIdError
    {
        InvalidId,
        NotFound
    }

    public record GetPhotoGearById(Guid Id) : IRequest<Result<GetPhotoGearByIdError, PhotoGear>>;
}
