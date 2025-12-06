using Api.Dtos;
using Application.Crud.PhotoGears.Create;
using Application.Crud.PhotoGears.Delete;
using Application.Crud.PhotoGears.Read;
using Application.Crud.PhotoGears.Update;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoGearController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PhotoGearDto>> Create([FromBody] CreatePhotoGearDto dto, CancellationToken cancellationToken)
    {
        var command = new AddPhotoGearCommand
        {
            Name = dto.Name,
            Type = dto.Type,
            Model = dto.Model,
            SerialNumber = dto.SerialNumber,
            PhotographerId = dto.PhotographerId
        };

        var result = await mediator.Send(command, cancellationToken);
        return Ok(PhotoGearDto.FromDomainModel(result));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PhotoGearDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetPhotoGearById(id));
        return result is null ? NotFound() : Ok(PhotoGearDto.FromDomainModel(result));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await mediator.Send(new DeletePhotoGearCommand(id));
        return success ? NoContent() : NotFound();
    }
    [HttpGet]
    public async Task<ActionResult<List<PhotoGearDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllPhotoGears());
        return Ok(result.Select(PhotoGearDto.FromDomainModel).ToList());
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PhotoGearDto>> Update(Guid id, [FromBody] UpdatePhotoGearDto dto)
    {
        var command = new UpdatePhotoGearCommand
        {
            Id = id,
            Name = dto.Name,
            Model = dto.Model,
            Status = dto.Status
        };

        var result = await mediator.Send(command);
        return Ok(PhotoGearDto.FromDomainModel(result));
    }
}
