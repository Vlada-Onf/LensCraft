using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PortfolioItems.Create
{
    public class AddPortfolioItemCommandHandler(IPortfolioService portfolioService)
    : IRequestHandler<AddPortfolioItemCommand, Result<AddPortfolioItemError, PortfolioItem>>
    {
        public async Task<Result<AddPortfolioItemError, PortfolioItem>> Handle(
            AddPortfolioItemCommand request,
            CancellationToken cancellationToken)
        {
            if (request.PhotographerId == Guid.Empty)
                return Result<AddPortfolioItemError, PortfolioItem>.Fail(AddPortfolioItemError.InvalidPhotographerId);

            if (string.IsNullOrWhiteSpace(request.Title))
                return Result<AddPortfolioItemError, PortfolioItem>.Fail(AddPortfolioItemError.InvalidTitle);

            if (string.IsNullOrWhiteSpace(request.Description))
                return Result<AddPortfolioItemError, PortfolioItem>.Fail(AddPortfolioItemError.InvalidDescription);

            if (string.IsNullOrWhiteSpace(request.ImageUrl))
                return Result<AddPortfolioItemError, PortfolioItem>.Fail(AddPortfolioItemError.InvalidImageUrl);

            var item = await portfolioService.AddAsync(
                Guid.NewGuid(),
                request.PhotographerId,
                request.Title,
                request.Description,
                request.ImageUrl,
                request.Location);

            return Result<AddPortfolioItemError, PortfolioItem>.Success(item);
        }
    }
}
