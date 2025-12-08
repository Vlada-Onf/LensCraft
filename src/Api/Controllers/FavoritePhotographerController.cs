using Api.Dtos;
using Application.Crud.FavoritePhotographers.Create;
using Application.Crud.FavoritePhotographers.Delete;
using Application.Crud.FavoritePhotographers.Read;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritePhotographerController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<FavoritePhotographerDto>> Add([FromBody] AddFavoritePhotographerDto dto)
        {
            var command = new AddFavoritePhotographerCommand
            {
                UserId = dto.UserId,
                PhotographerId = dto.PhotographerId
            };
            var result = await mediator.Send(command);

            if (!result.IsSuccess)
            {
                if (result.Error == AddFavoritePhotographerError.AlreadyFavorited)
                    return Conflict("Already favorited");

                return BadRequest(result.Error);
            }

            return Ok(FavoritePhotographerDto.FromDomainModel(result.Value!));
        }

        [HttpDelete("{userId:guid}/{photographerId:guid}")]
        public async Task<IActionResult> Remove(Guid userId, Guid photographerId)
        {
            var result = await mediator.Send(new DeleteFavoritePhotographerCommand(userId, photographerId));

            if (!result.IsSuccess)
            {
                if (result.Error == DeleteFavoritePhotographerError.NotFound)
                    return NotFound();

                return BadRequest(result.Error);
            }

            return NoContent();
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<List<FavoritePhotographerDto>>> GetUserFavorites(Guid userId)
        {
            var favorites = await mediator.Send(new GetUserFavoritePhotographers(userId));
            return Ok(favorites.Select(FavoritePhotographerDto.FromDomainModel).ToList());
        }

        [HttpGet("{userId:guid}/{photographerId:guid}")]
        public async Task<ActionResult<FavoritePhotographerDto>> Get(Guid userId, Guid photographerId)
        {
            var result = await mediator.Send(new GetFavoritePhotographer(userId, photographerId));

            if (!result.IsSuccess)
            {
                if (result.Error == GetFavoritePhotographerError.NotFound)
                    return NotFound();

                return BadRequest(result.Error);
            }

            return Ok(FavoritePhotographerDto.FromDomainModel(result.Value!));
        }
    }
}
