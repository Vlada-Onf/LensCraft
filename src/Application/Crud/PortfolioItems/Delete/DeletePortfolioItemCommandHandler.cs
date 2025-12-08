using Application.Common.Interfaces;
using Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PortfolioItems.Delete
{
    public class DeletePortfolioItemCommandHandler(IPortfolioService portfolioService)
    : IRequestHandler<DeletePortfolioItemCommand, Result<DeletePortfolioItemError, bool>>
    {
        public async Task<Result<DeletePortfolioItemError, bool>> Handle(
            DeletePortfolioItemCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<DeletePortfolioItemError, bool>.Fail(DeletePortfolioItemError.InvalidId);

            var deleted = await portfolioService.RemoveAsync(request.Id);
            if (!deleted)
                return Result<DeletePortfolioItemError, bool>.Fail(DeletePortfolioItemError.NotFound);

            return Result<DeletePortfolioItemError, bool>.Success(true);
        }
    }
}
