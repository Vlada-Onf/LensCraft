using Api.Dtos;
using Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Test.Tests.Common;
using Test.Tests.Data.User;
using Tests.Common;
using Tests.Common.Services;
using Xunit;
using Xunit.Abstractions;

namespace Api.Tests.Integration.User;

public class UserControllerTests : BaseIntegrationTest
{
    private readonly ITestOutputHelper _output;
    private readonly Domain.Models.User _firstUser;
    private readonly Domain.Models.User _secondUser;

    private const string BaseRoute = "api/user";

    public UserControllerTests(IntegrationTestWebFactory factory, ITestOutputHelper output) : base(factory)
    {
        _output = output;

        _firstUser = UserData.FirstUser();
        _secondUser = UserData.SecondUser();
    }

    [Fact]
    public async Task ShouldCreateUser()
    {
        var request = new CreateUserDto(
            _secondUser.FirstName,
            _secondUser.LastName,
            _secondUser.Email);

        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Status Code: {response.StatusCode}");
            _output.WriteLine($"Error Content: {errorContent}");
        }

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<UserDto>();

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var dbEntity = await freshContext.Users.FindAsync(dto.Id);

        dbEntity.Should().NotBeNull();
        dbEntity!.FirstName.Should().Be(_secondUser.FirstName);
        dbEntity.LastName.Should().Be(_secondUser.LastName);
        dbEntity.Email.Should().Be(_secondUser.Email);
    }

    [Fact]
    public async Task ShouldNotCreateUserWithInvalidEmail()
    {
        var request = new CreateUserDto(
            "Test",
            "User",
            "invalid-email");

        var response = await Client.PostAsJsonAsync(BaseRoute, request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldGetUserById()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{_firstUser.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.ToResponseModel<UserDto>();
        dto.Id.Should().Be(_firstUser.Id);
        dto.FirstName.Should().Be(_firstUser.FirstName);
        dto.LastName.Should().Be(_firstUser.LastName);
        dto.Email.Should().Be(_firstUser.Email);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenUserDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await Client.GetAsync($"{BaseRoute}/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldUpdateUser()
    {
        var request = new UpdateUserDto(
            "Updated First",
            "Updated Last");

        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{_firstUser.Id}", request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Status Code: {response.StatusCode}");
            _output.WriteLine($"Error Content: {errorContent}");
        }

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<UserDto>();

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var updated = await freshContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

        updated.Should().NotBeNull();
        updated!.FirstName.Should().Be("Updated First");
        updated.LastName.Should().Be("Updated Last");
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenUpdatingNonExistentUser()
    {
        var request = new UpdateUserDto(
            "Some",
            "Name");

        var nonExistentId = Guid.NewGuid();
        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{nonExistentId}", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldDeleteUser()
    {
        var response = await Client.DeleteAsync($"{BaseRoute}/{_firstUser.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var deleted = await freshContext.Users.FindAsync(_firstUser.Id);
        deleted.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenDeletingNonExistentUser()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await Client.DeleteAsync($"{BaseRoute}/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldGetAllUsers()
    {
        var response = await Client.GetAsync(BaseRoute);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var users = await response.ToResponseModel<List<UserDto>>();
        users.Should().NotBeEmpty();
        users.Should().Contain(u => u.Id == _firstUser.Id);
    }

    public override async Task InitializeAsync()
    {
        await Context.Users.AddAsync(_firstUser);
        await SaveChangesAsync();
    }

    public override async Task DisposeAsync()
    {
        Context.Users.RemoveRange(Context.Users);
        await SaveChangesAsync();
    }
}
