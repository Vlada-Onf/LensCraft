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
    public class GetAllPortfolioItemsHandler(IPortfolioService portfolioService)
    : IRequestHandler<GetAllPortfolioItems, IReadOnlyList<PortfolioItem>>
    {
        public async Task<IReadOnlyList<PortfolioItem>> Handle(GetAllPortfolioItems request, CancellationToken cancellationToken)
        {
            return await portfolioService.GetAllAsync();
        }
    }
}
