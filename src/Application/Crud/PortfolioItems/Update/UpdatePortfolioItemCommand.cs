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
    public enum UpdatePortfolioItemError
    {
        InvalidId,
        InvalidTitle,
        InvalidDescription,
        InvalidImageUrl,
        NotFound
    }

    public record UpdatePortfolioItemCommand : IRequest<Result<UpdatePortfolioItemError, PortfolioItem>>
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required string ImageUrl { get; init; }
        public string? Location { get; init; }
    }
}
