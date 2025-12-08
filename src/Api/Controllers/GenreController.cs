using Api.Dtos;
using Application.Crud.Genres.Create;
using Application.Crud.Genres.Delete;
using Application.Crud.Genres.Read;
using Application.Crud.Genres.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Results;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create([FromBody] CreateGenreDto dto)
        {
            var command = new AddGenreCommand { Name = dto.Name };
            var result = await mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(GenreDto.FromDomainModel(result.Value!));
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDto>>> GetAll()
        {
            var genres = await mediator.Send(new GetAllGenres());
            return Ok(genres.Select(GenreDto.FromDomainModel).ToList());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GenreDto>> GetById(Guid id)
        {
            var result = await mediator.Send(new GetGenreById(id));

            if (!result.IsSuccess)
            {
                if (result.Error == GetGenreByIdError.NotFound)
                    return NotFound();

                return BadRequest(result.Error);
            }

            return Ok(GenreDto.FromDomainModel(result.Value!));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GenreDto>> Update(Guid id, [FromBody] UpdateGenreDto dto)
        {
            var command = new UpdateGenreCommand { Id = id, Name = dto.Name };
            var result = await mediator.Send(command);

            if (!result.IsSuccess)
            {
                if (result.Error == UpdateGenreError.NotFound)
                    return NotFound();

                return BadRequest(result.Error);
            }

            return Ok(GenreDto.FromDomainModel(result.Value!));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await mediator.Send(new DeleteGenreCommand(id));

            if (!result.IsSuccess)
            {
                if (result.Error == DeleteGenreError.NotFound)
                    return NotFound();

                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
