using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PortfolioItems.Create
{
    public class AddPortfolioItemCommandHandler(
    IPortfolioService portfolioService
) : IRequestHandler<AddPortfolioItemCommand, PortfolioItem>
    {
        public async Task<PortfolioItem> Handle(AddPortfolioItemCommand request, CancellationToken cancellationToken)
        {
            var item = await portfolioService.AddAsync(
                Guid.NewGuid(),
                request.PhotographerId,
                request.Title,
                request.Description,
                request.ImageUrl,
                request.Location
            );

            return item;
        }
    }
}
