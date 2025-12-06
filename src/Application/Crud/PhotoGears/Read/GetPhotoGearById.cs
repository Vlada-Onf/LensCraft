using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Crud.PhotoGears.Read
{
    public record GetPhotoGearById(Guid Id) : IRequest<PhotoGear?>;
}
