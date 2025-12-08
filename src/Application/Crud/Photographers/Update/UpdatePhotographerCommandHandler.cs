using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;
using static Application.Crud.Photographers.Update.UpdatePhotographerCommand;

namespace Application.Crud.Photographers.Update;

public class UpdatePhotographerCommandHandler
    : IRequestHandler<UpdatePhotographerCommand, Result<UpdatePhotographerError, Photographer>>
{
    private readonly IPhotographerService _service;

    public UpdatePhotographerCommandHandler(IPhotographerService service)
    {
        _service = service;
    }

    public async Task<Result<UpdatePhotographerError, Photographer>> Handle(
        UpdatePhotographerCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            return Result<UpdatePhotographerError, Photographer>.Fail(
                UpdatePhotographerError.InvalidId);

        if (string.IsNullOrWhiteSpace(request.FirstName))
            return Result<UpdatePhotographerError, Photographer>.Fail(
                UpdatePhotographerError.InvalidFirstName);

        if (string.IsNullOrWhiteSpace(request.LastName))
            return Result<UpdatePhotographerError, Photographer>.Fail(
                UpdatePhotographerError.InvalidLastName);

        var photographer = await _service.GetByIdAsync(request.Id);

        if (photographer is null)
            return Result<UpdatePhotographerError, Photographer>.Fail(
                UpdatePhotographerError.NotFound);

        photographer.UpdateDetails(request.FirstName, request.LastName, request.Bio);

        await _service.SaveChangesAsync(); // або твій існуючий метод збереження

        return Result<UpdatePhotographerError, Photographer>.Success(photographer);
    }
}
