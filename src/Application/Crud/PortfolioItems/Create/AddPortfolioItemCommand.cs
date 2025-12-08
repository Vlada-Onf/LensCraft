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
    public enum AddPortfolioItemError
    {
        InvalidPhotographerId,
        InvalidTitle,
        InvalidDescription,
        InvalidImageUrl
    }

    public record AddPortfolioItemCommand : IRequest<Result<AddPortfolioItemError, PortfolioItem>>
    {
        public required Guid PhotographerId { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required string ImageUrl { get; init; }
        public string? Location { get; init; }
    }
}
