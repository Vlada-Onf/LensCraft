using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using Api.Dtos;
using Tests.Common;
using Tests.Data.Photographer;
using Tests.Data.PortfolioItem;
using Tests.Common.Extensions;
using PhotographerModel = Domain.Models.Photographer;

namespace Api.Tests.Integration.Photographer;

public class PhotographerControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private const string BaseRoute = "/api/Photographer";

    private readonly PhotographerModel _firstTestPhotographer;
    private readonly PhotographerModel _secondTestPhotographer;

    public PhotographerControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _firstTestPhotographer = PhotographerData.FirstPhotographer();
        _secondTestPhotographer = PhotographerData.SecondPhotographer();
    }

    public async Task InitializeAsync()
    {
        await Context.Photographers.AddRangeAsync(_firstTestPhotographer, _secondTestPhotographer);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Photographers.RemoveRange(Context.Photographers);
        await SaveChangesAsync();
    }

    [Fact]
    public async Task ShouldCreatePhotographer()
    {
        var request = new CreatePhotographerDto
        {
            FirstName = "Lina",
            LastName = "Sharp",
            Email = "lina@lens.com",
            Bio = "Fashion and editorial"
        };

        var response = await Client.PostAsJsonAsync(BaseRoute, request);
        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<PhotographerDto>();
        var dbEntity = await Context.Photographers.FindAsync(dto.Id);

        dbEntity.Should().NotBeNull();
        dbEntity!.FirstName.Should().Be(request.FirstName);
        dbEntity.LastName.Should().Be(request.LastName);
        dbEntity.Email.Should().Be(request.Email);
    }

    [Fact]
    public async Task ShouldNotCreatePhotographerWithInvalidData()
    {
        var request = new CreatePhotographerDto
        {
            FirstName = "",
            LastName = "",
            Email = "invalid-email",
            Bio = ""
        };

        var response = await Client.PostAsJsonAsync(BaseRoute, request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldUpdatePhotographer()
    {
        var request = new UpdatePhotographerDto
        {
            Id = _firstTestPhotographer.Id.Value,
            FirstName = "UpdatedName",
            LastName = _firstTestPhotographer.LastName,
            Email = _firstTestPhotographer.Email,
            Bio = _firstTestPhotographer.Bio
        };

        var response = await Client.PutAsJsonAsync(BaseRoute, request);
        response.IsSuccessStatusCode.Should().BeTrue();

        var updated = await Context.Photographers.FindAsync(_firstTestPhotographer.Id);
        updated!.FirstName.Should().Be("UpdatedName");
    }

    [Fact]
    public async Task ShouldNotUpdateWithInvalidData()
    {
        var request = new UpdatePhotographerDto
        {
            Id = _firstTestPhotographer.Id.Value,
            FirstName = "",
            LastName = "",
            Email = "invalid",
            Bio = ""
        };

        var response = await Client.PutAsJsonAsync(BaseRoute, request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenUpdatingNonExistentPhotographer()
    {
        var request = new UpdatePhotographerDto
        {
            Id = Guid.NewGuid(),
            FirstName = "Ghost",
            LastName = "User",
            Email = "ghost@lens.com",
            Bio = "No bio"
        };

        var response = await Client.PutAsJsonAsync(BaseRoute, request);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldDeletePhotographer()
    {
        var response = await Client.DeleteAsync($"{BaseRoute}/{_secondTestPhotographer.Id}");
        response.IsSuccessStatusCode.Should().BeTrue();

        var exists = await Context.Photographers.FindAsync(_secondTestPhotographer.Id);
        exists.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenDeletingNonExistentPhotographer()
    {
        var response = await Client.DeleteAsync($"{BaseRoute}/{Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldHandleCascadeDelete()
    {
        var portfolioItem = PortfolioItemData.FirstPortfolioItem(_firstTestPhotographer.Id);
        await Context.PortfolioItems.AddAsync(portfolioItem);
        await SaveChangesAsync();

        var response = await Client.DeleteAsync($"{BaseRoute}/{_firstTestPhotographer.Id}");
        response.IsSuccessStatusCode.Should().BeTrue();

        var exists = await Context.PortfolioItems.FindAsync(portfolioItem.Id);
        exists.Should().BeNull();
    }

    [Fact]
    public async Task ShouldGetPhotographerById()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{_firstTestPhotographer.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.ToResponseModel<PhotographerDto>();
        dto.Id.Should().Be(_firstTestPhotographer.Id.Value);
        dto.FirstName.Should().Be(_firstTestPhotographer.FirstName);
    }

    [Fact]
    public async Task ShouldUpdatePortfolioItemWithPhotographer()
    {
        var newPhotographer = PhotographerData.SecondPhotographer();
        await Context.Photographers.AddAsync(newPhotographer);
        await SaveChangesAsync();

        var portfolioItem = PortfolioItemData.FirstPortfolioItem(_firstTestPhotographer.Id);
        await Context.PortfolioItems.AddAsync(portfolioItem);
        await SaveChangesAsync();

        var request = new UpdatePortfolioItemDto
        {
            Id = portfolioItem.Id.Value,
            PhotographerId = newPhotographer.Id.Value,
            Title = portfolioItem.Title,
            Description = portfolioItem.Description,
            ImageUrl = portfolioItem.ImageUrl,
            Location = portfolioItem.Location,
            PublishedAt = portfolioItem.PublishedAt
        };

        var response = await Client.PutAsJsonAsync("/api/PortfolioItem", request);
        response.IsSuccessStatusCode.Should().BeTrue();

        var updated = await Context.PortfolioItems.FindAsync(portfolioItem.Id);
        updated!.PhotographerId.Should().Be(newPhotographer.Id);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenPhotographerDoesNotExist()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
