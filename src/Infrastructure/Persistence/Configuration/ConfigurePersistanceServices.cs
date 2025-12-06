using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Configuration
{
    public static class ConfigurePersistanceServices
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            var connectionString = "Server=localhost; Port=5432; Database=lensCraft-db; User Id=postgres; Password=1234";
            var dataSourceBuilder = new Npgsql.NpgsqlDataSourceBuilder(connectionString);
            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    dataSource,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                )
                .ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.CoreEventId.ManyServiceProvidersCreatedWarning))
            );
        }
    }
}