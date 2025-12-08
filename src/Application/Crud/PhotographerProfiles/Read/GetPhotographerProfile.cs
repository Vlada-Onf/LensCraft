using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotographerProfiles.Read
{
    public enum GetPhotographerProfileError
    {
        InvalidPhotographerId,
        NotFound
    }

    public record GetPhotographerProfile(Guid PhotographerId) : IRequest<Result<GetPhotographerProfileError, PhotographerProfile>>;
}
