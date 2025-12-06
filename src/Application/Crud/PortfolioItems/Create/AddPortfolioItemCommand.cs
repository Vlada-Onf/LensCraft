using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Crud.PortfolioItems.Create
{
    public record AddPortfolioItemCommand : IRequest<PortfolioItem>
    {
        public required Guid PhotographerId { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required string ImageUrl { get; init; }
        public string? Location { get; init; }
    }
}
