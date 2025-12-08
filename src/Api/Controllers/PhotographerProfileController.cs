using Api.Dtos;
using Application.Crud.PhotographerProfiles.Create;
using Application.Crud.PhotographerProfiles.Read;
using Application.Crud.PhotographerProfiles.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotographerProfileController(IMediator mediator) : ControllerBase
    {
        [HttpPost("{photographerId:guid}")]
        public async Task<ActionResult<PhotographerProfileDto>> Create(Guid photographerId, [FromBody] CreatePhotographerProfileDto dto)
        {
            var command = new CreatePhotographerProfileCommand
            {
                PhotographerId = photographerId,
                Phone = dto.Phone,
                Website = dto.Website,
                Instagram = dto.Instagram
            };

            var result = await mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(PhotographerProfileDto.FromDomainModel(result.Value!));
        }

        [HttpPut("{photographerId:guid}")]
        public async Task<ActionResult<PhotographerProfileDto>> Update(Guid photographerId, [FromBody] UpdatePhotographerProfileDto dto)
        {
            var command = new UpdatePhotographerProfileCommand
            {
                PhotographerId = photographerId,
                Phone = dto.Phone,
                Website = dto.Website,
                Instagram = dto.Instagram
            };

            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                if (result.Error == UpdatePhotographerProfileError.NotFound)
                    return NotFound();

                return BadRequest(result.Error);
            }

            return Ok(PhotographerProfileDto.FromDomainModel(result.Value!));
        }

        [HttpGet("{photographerId:guid}")]
        public async Task<ActionResult<PhotographerProfileDto>> Get(Guid photographerId)
        {
            var result = await mediator.Send(new GetPhotographerProfile(photographerId));
            if (!result.IsSuccess)
            {
                if (result.Error == GetPhotographerProfileError.NotFound)
                    return NotFound();

                return BadRequest(result.Error);
            }

            return Ok(PhotographerProfileDto.FromDomainModel(result.Value!));
        }
    }
}
