using Infrastructure.Persistence;
using Infrastructure.Persistence.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace Tests.Common;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebFactory>
{
    protected readonly ApplicationDbContext Context;
    protected readonly HttpClient Client;

    protected BaseIntegrationTest(IntegrationTestWebFactory factory)
    {
        // 🔧 Отримуємо scope для доступу до сервісів
        var scope = factory.Services.CreateScope();
        Context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // 🌐 Створюємо HTTP-клієнт для запитів до API
        Client = factory.CreateClient();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // 🔐 Якщо буде автентифікація — тут можна додати токен або заголовки
        // Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
    }

    protected async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }
}
