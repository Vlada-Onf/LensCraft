using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PortfolioItems.Update
{
    public class UpdatePortfolioItemCommandHandler(IPortfolioService portfolioService)
    : IRequestHandler<UpdatePortfolioItemCommand, Result<UpdatePortfolioItemError, PortfolioItem>>
    {
        public async Task<Result<UpdatePortfolioItemError, PortfolioItem>> Handle(
            UpdatePortfolioItemCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<UpdatePortfolioItemError, PortfolioItem>.Fail(UpdatePortfolioItemError.InvalidId);

            if (string.IsNullOrWhiteSpace(request.Title))
                return Result<UpdatePortfolioItemError, PortfolioItem>.Fail(UpdatePortfolioItemError.InvalidTitle);

            if (string.IsNullOrWhiteSpace(request.Description))
                return Result<UpdatePortfolioItemError, PortfolioItem>.Fail(UpdatePortfolioItemError.InvalidDescription);

            if (string.IsNullOrWhiteSpace(request.ImageUrl))
                return Result<UpdatePortfolioItemError, PortfolioItem>.Fail(UpdatePortfolioItemError.InvalidImageUrl);

            var item = await portfolioService.GetByIdAsync(request.Id);
            if (item is null)
                return Result<UpdatePortfolioItemError, PortfolioItem>.Fail(UpdatePortfolioItemError.NotFound);

            item.UpdateDetails(request.Title, request.Description, request.ImageUrl, request.Location);
            await portfolioService.SaveAsync();

            return Result<UpdatePortfolioItemError, PortfolioItem>.Success(item);
        }
    }
}