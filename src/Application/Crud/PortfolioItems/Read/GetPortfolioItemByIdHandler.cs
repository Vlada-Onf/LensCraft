using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PortfolioItems.Read
{
    public class GetPortfolioItemByIdQueryHandler(
    IPortfolioService portfolioService
) : IRequestHandler<GetPortfolioItemByIdQuery, PortfolioItem?>
    {
        public async Task<PortfolioItem?> Handle(GetPortfolioItemByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Id must not be empty");

            return await portfolioService.GetByIdAsync(request.Id);
        }
    }


}
