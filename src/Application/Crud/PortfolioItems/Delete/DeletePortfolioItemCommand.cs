using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PortfolioItems.Delete
{
    public enum DeletePortfolioItemError
    {
        InvalidId,
        NotFound
    }

    public record DeletePortfolioItemCommand(Guid Id) : IRequest<Result<DeletePortfolioItemError, bool>>;
}
