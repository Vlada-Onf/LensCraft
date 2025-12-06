using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;


namespace Application.Crud.Users.Read
{
    public record GetAllUsers() : IRequest<IReadOnlyList<User>>;
}
