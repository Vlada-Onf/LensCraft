using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PhotoGears.Delete
{
    public record DeletePhotoGearCommand(Guid Id) : IRequest<bool>;
}
