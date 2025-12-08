using Application.Common.Interfaces;
using Application.Common.Results;
using Domain.Models;
using MediatR;

namespace Application.Crud.PhotoGears.Create;

public class AddPhotoGearCommandHandler(IPhotoGearService photoGearService)
    : IRequestHandler<AddPhotoGearCommand, Result<AddPhotoGearError, PhotoGear>>
{
    public async Task<Result<AddPhotoGearError, PhotoGear>> Handle(
        AddPhotoGearCommand request,
        CancellationToken cancellationToken)
    {
        if (request.PhotographerId == Guid.Empty)
            return Result<AddPhotoGearError, PhotoGear>.Fail(AddPhotoGearError.InvalidPhotographerId);

        if (string.IsNullOrWhiteSpace(request.Name))
            return Result<AddPhotoGearError, PhotoGear>.Fail(AddPhotoGearError.InvalidName);

        if (string.IsNullOrWhiteSpace(request.Type))
            return Result<AddPhotoGearError, PhotoGear>.Fail(AddPhotoGearError.InvalidType);

        if (string.IsNullOrWhiteSpace(request.Model))
            return Result<AddPhotoGearError, PhotoGear>.Fail(AddPhotoGearError.InvalidModel);

        if (string.IsNullOrWhiteSpace(request.SerialNumber))
            return Result<AddPhotoGearError, PhotoGear>.Fail(AddPhotoGearError.InvalidSerialNumber);

        var gear = await photoGearService.AddAsync(
            Guid.NewGuid(),
            request.PhotographerId,
            request.Name,
            request.Type,
            request.Model,
            request.SerialNumber);

        return Result<AddPhotoGearError, PhotoGear>.Success(gear);
    }
}