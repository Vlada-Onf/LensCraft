using MediatR;
using Application.Common.Interfaces;
using Domain.Models;

namespace Application.Crud.PhotoGears.Create;

public class AddPhotoGearCommandHandler(
    IPhotoGearService photoGearService
) : IRequestHandler<AddPhotoGearCommand, PhotoGear>
{
    public async Task<PhotoGear> Handle(AddPhotoGearCommand request, CancellationToken cancellationToken)
    {
        var gear = await photoGearService.AddAsync(
            Guid.NewGuid(),
            request.PhotographerId,
            request.Name,
            request.Type,
            request.Model,
            request.SerialNumber
        );

        return gear;
    }
}