using Api.Dtos;
using Application.Crud.PortfolioItems.Create;
using Application.Crud.PortfolioItems.Update;
using Application.Crud.PortfolioItems.Read;
using Application.Crud.PortfolioItems.Delete;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfolioItemController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PortfolioItemDto>> Create([FromBody] CreatePortfolioItemDto dto)
    {
        var command = new AddPortfolioItemCommand
        {
            PhotographerId = dto.PhotographerId,
            Title = dto.Title,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            Location = dto.Location
        };

        var result = await mediator.Send(command);
        return Ok(PortfolioItemDto.FromDomainModel(result));
    }
    [HttpGet]
    public async Task<ActionResult<List<PortfolioItemDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllPortfolioItems());
        return Ok(result.Select(PortfolioItemDto.FromDomainModel).ToList());
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PortfolioItemDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetPortfolioItemByIdQuery(id));
        return result is null ? NotFound() : Ok(PortfolioItemDto.FromDomainModel(result));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PortfolioItemDto>> Update(Guid id, [FromBody] UpdatePortfolioItemDto dto)
    {
        var command = new UpdatePortfolioItemCommand
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            Location = dto.Location
        };

        var result = await mediator.Send(command);
        return Ok(PortfolioItemDto.FromDomainModel(result));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await mediator.Send(new DeletePortfolioItemCommand(id));
        return success ? NoContent() : NotFound();
    }
}
