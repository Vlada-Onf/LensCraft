using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PortfolioItems.Delete
{
   public record DeletePortfolioItemCommand(Guid Id) : IRequest<bool>;
}
