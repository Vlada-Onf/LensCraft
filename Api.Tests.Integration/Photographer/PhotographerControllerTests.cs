using Api.Dtos;
using Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Test.Tests.Common;
using Test.Tests.Data.Photographer;
using Tests.Common;
using Tests.Common.Services;
using Xunit;
using Xunit.Abstractions;

namespace Api.Tests.Integration.Photographer;

public class PhotographerControllerTests : BaseIntegrationTest
{
    private readonly ITestOutputHelper _output;
    private readonly Domain.Models.Photographer _firstPhotographer;
    private readonly Domain.Models.Photographer _secondPhotographer;

    private const string BaseRoute = "api/photographer";

    public PhotographerControllerTests(IntegrationTestWebFactory factory, ITestOutputHelper output) : base(factory)
    {
        _output = output;

        _firstPhotographer = PhotographerData.FirstPhotographer();
        _secondPhotographer = PhotographerData.SecondPhotographer();
    }

    [Fact]
    public async Task ShouldCreatePhotographer()
    {
        var request = new CreatePhotographerDto(
            _secondPhotographer.FirstName,
            _secondPhotographer.LastName,
            _secondPhotographer.Email,
            _secondPhotographer.Bio);

        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Status Code: {response.StatusCode}");
            _output.WriteLine($"Error Content: {errorContent}");
        }

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<PhotographerDto>();

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var dbEntity = await freshContext.Photographers.FindAsync(dto.Id);

        dbEntity.Should().NotBeNull();
        dbEntity!.FirstName.Should().Be(_secondPhotographer.FirstName);
        dbEntity.LastName.Should().Be(_secondPhotographer.LastName);
        dbEntity.Email.Should().Be(_secondPhotographer.Email);
        dbEntity.Bio.Should().Be(_secondPhotographer.Bio);
    }

    [Fact]
    public async Task ShouldNotCreatePhotographerWithInvalidEmail()
    {
        var request = new CreatePhotographerDto(
            "Test",
            "User",
            "invalid-email",
            "Test bio");

        var response = await Client.PostAsJsonAsync(BaseRoute, request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldGetPhotographerById()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{_firstPhotographer.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.ToResponseModel<PhotographerDto>();
        dto.Id.Should().Be(_firstPhotographer.Id);
        dto.FirstName.Should().Be(_firstPhotographer.FirstName);
        dto.LastName.Should().Be(_firstPhotographer.LastName);
        dto.Email.Should().Be(_firstPhotographer.Email);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenPhotographerDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await Client.GetAsync($"{BaseRoute}/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldUpdatePhotographer()
    {
        var request = new UpdatePhotographerDto(
            "Updated First",
            "Updated Last",
            "Updated bio with new information");

        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{_firstPhotographer.Id}", request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Status Code: {response.StatusCode}");
            _output.WriteLine($"Error Content: {errorContent}");
        }

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<PhotographerDto>();

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var updated = await freshContext.Photographers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

        updated.Should().NotBeNull();
        updated!.FirstName.Should().Be("Updated First");
        updated.LastName.Should().Be("Updated Last");
        updated.Bio.Should().Be("Updated bio with new information");
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenUpdatingNonExistentPhotographer()
    {
        var request = new UpdatePhotographerDto(
            "Some",
            "Name",
            "Some bio");

        var nonExistentId = Guid.NewGuid();
        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{nonExistentId}", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldDeletePhotographer()
    {
        var response = await Client.DeleteAsync($"{BaseRoute}/{_firstPhotographer.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var deleted = await freshContext.Photographers.FindAsync(_firstPhotographer.Id);
        deleted.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenDeletingNonExistentPhotographer()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await Client.DeleteAsync($"{BaseRoute}/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldGetAllPhotographers()
    {
        var response = await Client.GetAsync(BaseRoute);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var photographers = await response.ToResponseModel<List<PhotographerDto>>();
        photographers.Should().NotBeEmpty();
        photographers.Should().Contain(p => p.Id == _firstPhotographer.Id);
    }

    public override async Task InitializeAsync()
    {
        await Context.Photographers.AddAsync(_firstPhotographer);
        await SaveChangesAsync();
    }

    public override async Task DisposeAsync()
    {
        Context.Photographers.RemoveRange(Context.Photographers);
        await SaveChangesAsync();
    }
}
