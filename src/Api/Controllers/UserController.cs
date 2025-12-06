using Api.Dtos;
using Application.Crud.Users.Create;
using Application.Crud.Users.Update;
using Application.Crud.Users.Read;
using Application.Crud.Users.Delete;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
    {
        var command = new AddUserCommand
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };

        var result = await mediator.Send(command);
        return Ok(UserDto.FromDomainModel(result));
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllUsers());
        return Ok(result.Select(UserDto.FromDomainModel).ToList());
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetUserByIdQuery(id));
        return result is null ? NotFound() : Ok(UserDto.FromDomainModel(result));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto dto)
    {
        var command = new UpdateUserCommand
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var result = await mediator.Send(command);
        return Ok(UserDto.FromDomainModel(result));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await mediator.Send(new DeleteUserCommand(id));
        return success ? NoContent() : NotFound();
    }
}
