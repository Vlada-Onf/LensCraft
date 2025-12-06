using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.PortfolioItems.Delete
{
    public class DeletePortfolioItemCommandHandler(
    IPortfolioService portfolioService
) : IRequestHandler<DeletePortfolioItemCommand, bool>
    {
        public async Task<bool> Handle(DeletePortfolioItemCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Id must not be empty");

            return await portfolioService.RemoveAsync(request.Id);
        }

    }
}
