using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using MediatR;

namespace Application.Crud.Comments.Read;

public record GetAllComments() : IRequest<IReadOnlyList<Comment>>;
