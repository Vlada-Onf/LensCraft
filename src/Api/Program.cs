using Application.Common.Interfaces;
using Application.Common.Behaviors;
using Application.Crud.Comments.Create;
using Application.Crud.PhotoGears.Create;
using Application.Crud.Photographers.Create;
using Application.Crud.PortfolioItems.Create;
using Application.Crud.Users.Create;
using Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;
using Infrastructure.Persistence.Configuration;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<AddCommentCommandValidator>();

        builder.Services.AddPersistanceServices();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
                policy.SetIsOriginAllowed(_ => true)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials());
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "LensCraft API",
                Version = "v1",
                Description = "Portfolio network for photographers with location and gear info"
            });
        });

        builder.Services.AddScoped<IPhotographerService, PhotographerService>();
        builder.Services.AddScoped<IPhotoGearService, PhotoGearService>();
        builder.Services.AddScoped<IPortfolioService, PortfolioService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<IPhotographerProfileService, PhotographerProfileService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<IFavoritePhotographerService, FavoritePhotographerService>();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<AddPortfolioItemCommandHandler>();
            cfg.RegisterServicesFromAssemblyContaining<AddPhotographerCommandHandler>();
            cfg.RegisterServicesFromAssemblyContaining<AddPhotoGearCommandHandler>();
            cfg.RegisterServicesFromAssemblyContaining<AddUserCommandHandler>();
            cfg.RegisterServicesFromAssemblyContaining<AddCommentCommandHandler>();

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LensCraft API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.Use(async (context, next) =>
        {
            try
            {
                await next();
            }
            catch (FluentValidation.ValidationException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors.Select(e => new
                {
                    property = e.PropertyName,
                    error = e.ErrorMessage
                });

                await context.Response.WriteAsJsonAsync(new { errors });
            }
            catch (KeyNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Internal server error",
                    details = app.Environment.IsDevelopment() ? ex.Message : null
                });
            }
        });

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
