using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PortfolioItems.Read
{
    public enum GetPortfolioItemByIdError
    {
        InvalidId,
        NotFound
    }

    public record GetPortfolioItemByIdQuery(Guid Id) : IRequest<Result<GetPortfolioItemByIdError, PortfolioItem>>;
}
