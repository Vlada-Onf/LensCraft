using Application.Common.Behaviors;
using Application.Common.Interfaces;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // ЗМІНИВ Singleton на Scoped!
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPhotographerService, PhotographerService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPhotoGearService, PhotoGearService>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(IUserService).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(IUserService).Assembly);

            return services;
        }
    }
}
