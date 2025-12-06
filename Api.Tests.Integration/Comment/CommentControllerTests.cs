using Api.Dtos;
using Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Test.Tests.Common;
using Test.Tests.Data.Comment;
using Test.Tests.Data.Photographer;
using Test.Tests.Data.PortfolioItem;
using Test.Tests.Data.User;
using Tests.Common;
using Tests.Common.Services;
using Xunit;
using Xunit.Abstractions;

namespace Api.Tests.Integration.Comment;

public class CommentControllerTests : BaseIntegrationTest
{
    private readonly ITestOutputHelper _output;
    private readonly Domain.Models.User _testUser;
    private readonly Domain.Models.Photographer _testPhotographer;
    private readonly Domain.Models.PortfolioItem _testPortfolioItem;
    private readonly Domain.Models.Comment _firstComment;
    private readonly Domain.Models.Comment _secondComment;

    private const string BaseRoute = "api/comment";

    public CommentControllerTests(IntegrationTestWebFactory factory, ITestOutputHelper output) : base(factory)
    {
        _output = output;

        _testUser = UserData.FirstUser();
        _testPhotographer = PhotographerData.FirstPhotographer();
        _testPortfolioItem = PortfolioItemData.FirstPortfolioItem(_testPhotographer.Id);
        _firstComment = CommentData.FirstTestComment(_testPortfolioItem.Id, _testUser.Id);
        _secondComment = CommentData.SecondTestComment(_testPortfolioItem.Id, _testUser.Id);
    }

    [Fact]
    public async Task ShouldCreateComment()
    {
        var request = new CreateCommentDto(
            _secondComment.PortfolioItemId,
            _secondComment.AuthorId,
            _secondComment.Text);

        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Status Code: {response.StatusCode}");
            _output.WriteLine($"Error Content: {errorContent}");
        }

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<CommentDto>();

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var dbComment = await freshContext.Comments.FirstOrDefaultAsync(x => x.Id == dto.Id);

        dbComment.Should().NotBeNull();
        dbComment!.Text.Should().Be(_secondComment.Text);
        dbComment.PortfolioItemId.Should().Be(_secondComment.PortfolioItemId);
        dbComment.AuthorId.Should().Be(_secondComment.AuthorId);
    }

    [Fact]
    public async Task ShouldNotCreateCommentWithEmptyText()
    {
        var request = new CreateCommentDto(
            _testPortfolioItem.Id,
            _testUser.Id,
            "");

        var response = await Client.PostAsJsonAsync(BaseRoute, request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldGetCommentById()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{_firstComment.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.ToResponseModel<CommentDto>();
        dto.Id.Should().Be(_firstComment.Id);
        dto.Text.Should().Be(_firstComment.Text);
        dto.PortfolioItemId.Should().Be(_firstComment.PortfolioItemId);
        dto.AuthorId.Should().Be(_firstComment.AuthorId);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenCommentDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await Client.GetAsync($"{BaseRoute}/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldUpdateComment()
    {
        var newText = "Updated comment text";
        var request = new EditCommentDto(newText);

        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{_firstComment.Id}", request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Status Code: {response.StatusCode}");
            _output.WriteLine($"Error Content: {errorContent}");
        }

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<CommentDto>();

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var updated = await freshContext.Comments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

        updated.Should().NotBeNull();
        updated!.Text.Should().Be(newText);
    }

    [Fact]
    public async Task ShouldNotUpdateCommentWithEmptyText()
    {
        var request = new EditCommentDto("");

        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{_firstComment.Id}", request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldDeleteComment()
    {
        var response = await Client.DeleteAsync($"{BaseRoute}/{_firstComment.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        using var scope = Factory.Services.CreateScope();
        var freshContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.Configuration.ApplicationDbContext>();

        var deleted = await freshContext.Comments.FindAsync(_firstComment.Id);
        deleted.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenDeletingNonExistentComment()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await Client.DeleteAsync($"{BaseRoute}/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldGetAllComments()
    {
        var response = await Client.GetAsync(BaseRoute);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var comments = await response.ToResponseModel<List<CommentDto>>();
        comments.Should().NotBeEmpty();
        comments.Should().Contain(c => c.Id == _firstComment.Id);
    }

    public override async Task InitializeAsync()
    {
        await Context.Users.AddAsync(_testUser);
        await Context.Photographers.AddAsync(_testPhotographer);
        await SaveChangesAsync();

        await Context.PortfolioItems.AddAsync(_testPortfolioItem);
        await SaveChangesAsync();

        await Context.Comments.AddAsync(_firstComment);
        await SaveChangesAsync();
    }

    public override async Task DisposeAsync()
    {
        Context.Comments.RemoveRange(Context.Comments);
        await SaveChangesAsync();

        Context.PortfolioItems.RemoveRange(Context.PortfolioItems);
        await SaveChangesAsync();

        Context.Users.RemoveRange(Context.Users);
        Context.Photographers.RemoveRange(Context.Photographers);
        await SaveChangesAsync();
    }
}
