using Api.Dtos;
using Application.Crud.Photographers.Create;
using Application.Crud.Photographers.Update;
using Application.Crud.Photographers.Read;
using Application.Crud.Photographers.Delete;
using Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotographerController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PhotographerDto>> Create([FromBody] CreatePhotographerDto dto)
    {
        var command = new AddPhotographerCommand
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Bio = dto.Bio
        };

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(PhotographerDto.FromDomainModel(result.Value!));
    }

    [HttpGet]
    public async Task<ActionResult<List<PhotographerDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllPhotographers());
        return Ok(result.Select(PhotographerDto.FromDomainModel).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PhotographerDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetPhotographerById(id));

        if (!result.IsSuccess)
        {
            if (result.Error == GetPhotographerByIdError.NotFound)
                return NotFound();

            return BadRequest(result.Error);
        }

        return Ok(PhotographerDto.FromDomainModel(result.Value!));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PhotographerDto>> Update(Guid id, [FromBody] UpdatePhotographerDto dto)
    {
        var command = new UpdatePhotographerCommand
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Bio = dto.Bio
        };

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            if (result.Error == UpdatePhotographerError.NotFound)
                return NotFound();

            return BadRequest(result.Error);
        }

        return Ok(PhotographerDto.FromDomainModel(result.Value!));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeletePhotographerCommand(id));

        if (!result.IsSuccess)
        {
            if (result.Error == DeletePhotographerError.NotFound)
                return NotFound();

            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
