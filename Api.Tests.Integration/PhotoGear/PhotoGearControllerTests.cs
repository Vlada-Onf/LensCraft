using Api.Dtos;
using Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Test.Tests.Common;
using Test.Tests.Data.PhotoGear;
using Test.Tests.Data.Photographer;
using Tests.Common;
using Tests.Common.Services;
using Xunit;
using Xunit.Abstractions;

namespace Api.Tests.Integration.PhotoGear;

public class PhotoGearControllerTests : BaseIntegrationTest
{
    private readonly ITestOutputHelper _output;
    private readonly Domain.Models.Photographer _testPhotographer;
    private readonly Domain.Models.PhotoGear _firstTestGear;
    private readonly Domain.Models.PhotoGear _secondTestGear;

    private const string BaseRoute = "api/photogear";

    public PhotoGearControllerTests(IntegrationTestWebFactory factory, ITestOutputHelper output) : base(factory)
    {
        _output = output;

        _testPhotographer = PhotographerData.FirstPhotographer();
        _firstTestGear = PhotoGearData.FirstTestGear(_testPhotographer.Id);
        _secondTestGear = PhotoGearData.SecondTestGear(_testPhotographer.Id);
    }

    [Fact]
    public async Task ShouldCreatePhotoGear()
    {
        var request = new CreatePhotoGearDto(
            _secondTestGear.Name,
            _secondTestGear.Type,
            _secondTestGear.Model,
            _secondTestGear.SerialNumber,
            _secondTestGear.PhotographerId);

        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Status Code: {response.StatusCode}");
            _output.WriteLine($"Error Content: {errorContent}");
        }

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<PhotoGearDto>();

       /* using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();
*/
        var dbEntity = await Context.PhotoGears.FindAsync(dto.Id);

        dbEntity.Should().NotBeNull();
        dbEntity!.Name.Should().Be(_secondTestGear.Name);
        dbEntity.Type.Should().Be(_secondTestGear.Type);
        dbEntity.Model.Should().Be(_secondTestGear.Model);
    }

    [Fact]
    public async Task ShouldGetPhotoGearById()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{_firstTestGear.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.ToResponseModel<PhotoGearDto>();
        dto.Id.Should().Be(_firstTestGear.Id);
        dto.Name.Should().Be(_firstTestGear.Name);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenPhotoGearDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await Client.GetAsync($"{BaseRoute}/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldUpdatePhotoGear()
    {
        var request = new UpdatePhotoGearDto(
            "Updated Camera Name",
            "Updated Model X",
            GearStatus.UnderMaintenance);

        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{_firstTestGear.Id}", request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Status Code: {response.StatusCode}");
            _output.WriteLine($"Error Content: {errorContent}");
        }

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<PhotoGearDto>();

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var updated = await freshContext.PhotoGears
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

        updated.Should().NotBeNull();
        updated!.Name.Should().Be("Updated Camera Name");
        updated.Model.Should().Be("Updated Model X");
        updated.Status.Should().Be(GearStatus.UnderMaintenance);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenUpdatingNonExistentPhotoGear()
    {
        var request = new UpdatePhotoGearDto(
            "Some Name",
            "Some Model",
            GearStatus.Operational);

        var nonExistentId = Guid.NewGuid();
        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{nonExistentId}", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldDeletePhotoGear()
    {
        var response = await Client.DeleteAsync($"{BaseRoute}/{_firstTestGear.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var deleted = await freshContext.PhotoGears.FindAsync(_firstTestGear.Id);
        deleted.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenDeletingNonExistentPhotoGear()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await Client.DeleteAsync($"{BaseRoute}/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldGetAllPhotoGears()
    {
        var response = await Client.GetAsync(BaseRoute);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var gears = await response.ToResponseModel<List<PhotoGearDto>>();
        gears.Should().NotBeEmpty();
        gears.Should().Contain(g => g.Id == _firstTestGear.Id);
    }

    public override async Task InitializeAsync()
    {
        await Context.Photographers.AddAsync(_testPhotographer);
        await SaveChangesAsync();

        await Context.PhotoGears.AddAsync(_firstTestGear);
        await SaveChangesAsync();
    }

    public override async Task DisposeAsync()
    {
        Context.PhotoGears.RemoveRange(Context.PhotoGears);
        await SaveChangesAsync();

        Context.Photographers.RemoveRange(Context.Photographers);
        await SaveChangesAsync();
    }
}
