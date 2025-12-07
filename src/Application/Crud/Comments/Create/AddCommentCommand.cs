using Application.Common.Results;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Crud.Comments.Create
{
    public record AddCommentCommand
    : IRequest<Result<AddCommentError, Comment>>
    {
        public required Guid PortfolioItemId { get; init; }
        public required Guid AuthorId { get; init; }
        public required string Text { get; init; }
    }
    public enum AddCommentError
    {
        InvalidPortfolioItemId,
        InvalidAuthorId
    }
}
