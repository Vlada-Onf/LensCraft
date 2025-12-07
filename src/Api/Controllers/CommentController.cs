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

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(CommentDto.FromDomainModel(result.Value!));
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CommentDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetCommentById(id));

        if (!result.IsSuccess)
        {
            if (result.Error == GetCommentByIdError.NotFound)
                return NotFound();
            return BadRequest(result.Error);
        }

        return Ok(CommentDto.FromDomainModel(result.Value!));
    }
    [HttpGet]
    public async Task<ActionResult<List<CommentDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllComments());

        if (!result.IsSuccess)
        {
          return BadRequest(result.Error);
        }

        var dtos = result.Value!.Select(CommentDto.FromDomainModel).ToList();
        return Ok(dtos);
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

        if (!result.IsSuccess)
        {
            if (result.Error == EditCommentError.NotFound)
                return NotFound();

            return BadRequest(result.Error);
        }

        return Ok(CommentDto.FromDomainModel(result.Value!));

    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteCommentCommand(id));

        if (!result.IsSuccess)
        {
            if (result.Error == DeleteCommentError.NotFound)
                return NotFound();

            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
