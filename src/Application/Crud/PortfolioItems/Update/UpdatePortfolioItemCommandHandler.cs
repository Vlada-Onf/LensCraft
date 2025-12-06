using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PortfolioItems.Update
{
    public class UpdatePortfolioItemCommandHandler(
    IPortfolioService portfolioService
) : IRequestHandler<UpdatePortfolioItemCommand, PortfolioItem>
    {
        public async Task<PortfolioItem> Handle(UpdatePortfolioItemCommand request, CancellationToken cancellationToken)
        {
            var item = await portfolioService.GetByIdAsync(request.Id);
            if (item is null)
                throw new KeyNotFoundException($"PortfolioItem with ID {request.Id} not found");

            item.UpdateDetails(request.Title, request.Description, request.ImageUrl, request.Location);
            await portfolioService.SaveAsync();

            return item;
        }

    }
}