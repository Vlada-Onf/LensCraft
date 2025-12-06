using Api.Dtos;
using Application.Crud.Comments.Create;
using Application.Crud.Comments.Update;
using Application.Crud.Comments.Read;
using Application.Crud.Comments.Delete;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create([FromBody] CreateCommentDto dto)
    {
        var command = new AddCommentCommand
        {
            PortfolioItemId = dto.PortfolioItemId,
            AuthorId = dto.AuthorId,
            Text = dto.Text
        };

        var result = await mediator.Send(command);
        return Ok(CommentDto.FromDomainModel(result));
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CommentDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetCommentById(id));
        return result is null ? NotFound() : Ok(CommentDto.FromDomainModel(result));
    }
    [HttpGet]
    public async Task<ActionResult<List<CommentDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllComments());
        return Ok(result.Select(CommentDto.FromDomainModel).ToList());
    }


    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CommentDto>> Edit(Guid id, [FromBody] EditCommentDto dto)
    {
        var command = new EditCommentCommand
        {
            Id = id,
            NewText = dto.Text
        };

        var result = await mediator.Send(command);
        return Ok(CommentDto.FromDomainModel(result));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await mediator.Send(new DeleteCommentCommand(id));
        return success ? NoContent() : NotFound();
    }
}
