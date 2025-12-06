using Infrastructure.Persistence.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Tests.Common;
using Xunit;

namespace Test.Tests.Common
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebFactory>, IAsyncLifetime
    {
        protected readonly ApplicationDbContext Context;
        protected readonly HttpClient Client;
        protected readonly IntegrationTestWebFactory Factory;

        protected BaseIntegrationTest(IntegrationTestWebFactory factory)
        {
            Factory = factory;

            var scope = factory.Services.CreateScope();
            Context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        }

        protected async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
            Context.ChangeTracker.Clear();
        }

        public virtual Task InitializeAsync() => Task.CompletedTask;

        public virtual Task DisposeAsync() => Task.CompletedTask;
    }
}
