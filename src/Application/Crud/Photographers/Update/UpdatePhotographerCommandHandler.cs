using Application.Common.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Crud.Photographers.Update;

public class UpdatePhotographerCommandHandler : IRequestHandler<UpdatePhotographerCommand, Photographer>
{
    private readonly IPhotographerService _service;

    public UpdatePhotographerCommandHandler(IPhotographerService service)
    {
        _service = service;
    }

    public async Task<Photographer> Handle(UpdatePhotographerCommand request, CancellationToken cancellationToken)
    {
        var photographer = await _service.GetByIdAsync(request.Id);

        if (photographer is null)
            throw new KeyNotFoundException($"Photographer with ID {request.Id} not found");

        photographer.UpdateDetails(request.FirstName, request.LastName, request.Bio);
        await _service.SaveAsync();

        return photographer;
    }
}
