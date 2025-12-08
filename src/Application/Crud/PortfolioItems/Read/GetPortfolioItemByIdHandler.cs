using Application.Common.Interfaces;
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
    public class GetPortfolioItemByIdQueryHandler(IPortfolioService portfolioService)
    : IRequestHandler<GetPortfolioItemByIdQuery, Result<GetPortfolioItemByIdError, PortfolioItem>>
    {
        public async Task<Result<GetPortfolioItemByIdError, PortfolioItem>> Handle(
            GetPortfolioItemByIdQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<GetPortfolioItemByIdError, PortfolioItem>.Fail(GetPortfolioItemByIdError.InvalidId);

            var item = await portfolioService.GetByIdAsync(request.Id);
            if (item is null)
                return Result<GetPortfolioItemByIdError, PortfolioItem>.Fail(GetPortfolioItemByIdError.NotFound);

            return Result<GetPortfolioItemByIdError, PortfolioItem>.Success(item);
        }
    }
}
