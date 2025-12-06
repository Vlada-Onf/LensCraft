using Api.Dtos;
using Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Test.Tests.Common;
using Test.Tests.Data.Photographer;
using Test.Tests.Data.PortfolioItem;
using Tests.Common;
using Tests.Common.Services;
using Xunit;
using Xunit.Abstractions;

namespace Api.Tests.Integration.PortfolioItem;

public class PortfolioItemControllerTests : BaseIntegrationTest
{
    private readonly ITestOutputHelper _output;
    private readonly Domain.Models.Photographer _testPhotographer;
    private readonly Domain.Models.PortfolioItem _firstItem;
    private readonly Domain.Models.PortfolioItem _secondItem;

    private const string BaseRoute = "api/portfolioitem";

    public PortfolioItemControllerTests(IntegrationTestWebFactory factory, ITestOutputHelper output) : base(factory)
    {
        _output = output;

        _testPhotographer = PhotographerData.FirstPhotographer();
        _firstItem = PortfolioItemData.FirstPortfolioItem(_testPhotographer.Id);
        _secondItem = PortfolioItemData.SecondPortfolioItem(_testPhotographer.Id);
    }

    [Fact]
    public async Task ShouldCreatePortfolioItem()
    {
        var request = new CreatePortfolioItemDto(
            _secondItem.Title,
            _secondItem.Description,
            _secondItem.ImageUrl,
            _secondItem.Location,
            _secondItem.PhotographerId);

        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Status Code: {response.StatusCode}");
            _output.WriteLine($"Error Content: {errorContent}");
        }

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<PortfolioItemDto>();

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var dbEntity = await freshContext.PortfolioItems.FindAsync(dto.Id);

        dbEntity.Should().NotBeNull();
        dbEntity!.Title.Should().Be(_secondItem.Title);
        dbEntity.Description.Should().Be(_secondItem.Description);
        dbEntity.ImageUrl.Should().Be(_secondItem.ImageUrl);
        dbEntity.Location.Should().Be(_secondItem.Location);
    }

    [Fact]
    public async Task ShouldNotCreatePortfolioItemWithEmptyTitle()
    {
        var request = new CreatePortfolioItemDto(
            "",
            "Description",
            "https://example.com/image.jpg",
            "Location",
            _testPhotographer.Id);

        var response = await Client.PostAsJsonAsync(BaseRoute, request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldGetPortfolioItemById()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{_firstItem.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.ToResponseModel<PortfolioItemDto>();
        dto.Id.Should().Be(_firstItem.Id);
        dto.Title.Should().Be(_firstItem.Title);
        dto.Description.Should().Be(_firstItem.Description);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenPortfolioItemDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await Client.GetAsync($"{BaseRoute}/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldUpdatePortfolioItem()
    {
        var request = new UpdatePortfolioItemDto(
            "Updated Portfolio Title",
            "Updated description with more details",
            "https://example.com/updated-image.jpg",
            "New York, USA");

        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{_firstItem.Id}", request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Status Code: {response.StatusCode}");
            _output.WriteLine($"Error Content: {errorContent}");
        }

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<PortfolioItemDto>();

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var updated = await freshContext.PortfolioItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

        updated.Should().NotBeNull();
        updated!.Title.Should().Be("Updated Portfolio Title");
        updated.Description.Should().Be("Updated description with more details");
        updated.ImageUrl.Should().Be("https://example.com/updated-image.jpg");
        updated.Location.Should().Be("New York, USA");
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenUpdatingNonExistentPortfolioItem()
    {
        var request = new UpdatePortfolioItemDto(
            "Title",
            "Description",
            "https://example.com/image.jpg",
            "Location");

        var nonExistentId = Guid.NewGuid();
        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{nonExistentId}", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldDeletePortfolioItem()
    {
        var response = await Client.DeleteAsync($"{BaseRoute}/{_firstItem.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var deleted = await freshContext.PortfolioItems.FindAsync(_firstItem.Id);
        deleted.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenDeletingNonExistentPortfolioItem()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await Client.DeleteAsync($"{BaseRoute}/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldGetAllPortfolioItems()
    {
        var response = await Client.GetAsync(BaseRoute);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var items = await response.ToResponseModel<List<PortfolioItemDto>>();
        items.Should().NotBeEmpty();
        items.Should().Contain(i => i.Id == _firstItem.Id);
    }

    public override async Task InitializeAsync()
    {
        await Context.Photographers.AddAsync(_testPhotographer);
        await SaveChangesAsync();

        await Context.PortfolioItems.AddAsync(_firstItem);
        await SaveChangesAsync();
    }

    public override async Task DisposeAsync()
    {
        Context.PortfolioItems.RemoveRange(Context.PortfolioItems);
        await SaveChangesAsync();

        Context.Photographers.RemoveRange(Context.Photographers);
        await SaveChangesAsync();
    }
}
