using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // ДОДАЙ ЛОГУВАННЯ ДЛЯ ПЕРЕВІРКИ:
        Console.WriteLine($"ValidationBehavior called for {typeof(TRequest).Name}");
        Console.WriteLine($"Validators count: {_validators.Count()}");

        if (!_validators.Any())
        {
            Console.WriteLine("No validators found!");
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        Console.WriteLine($"Validation failures count: {failures.Count}");
        foreach (var failure in failures)
        {
            Console.WriteLine($"- {failure.PropertyName}: {failure.ErrorMessage}");
        }

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}
