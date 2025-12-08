using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotographerProfiles.Update
{
    public enum UpdatePhotographerProfileError
    {
        InvalidPhotographerId,
        NotFound
    }

    public record UpdatePhotographerProfileCommand : IRequest<Result<UpdatePhotographerProfileError, PhotographerProfile>>
    {
        public required Guid PhotographerId { get; init; }
        public required string Phone { get; init; }
        public required string Website { get; init; }
        public required string Instagram { get; init; }
    }
}
