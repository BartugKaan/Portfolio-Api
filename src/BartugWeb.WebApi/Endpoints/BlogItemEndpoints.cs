using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Queries.GetAll;
using BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Queries.GetById;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.WebApi.Endpoints.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

public class BlogItemEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var blogItemGroup = app.MapGroup("/api/blog-items")
            .WithTags("BlogItems")
            .WithOpenApi();

        blogItemGroup.MapGet("/", GetAllBlogItems);
        blogItemGroup.MapGet("/{id}", GetBlogItemById);
        blogItemGroup.MapPost("/", CreateBlogItem).RequireAuthorization();
        blogItemGroup.MapPut("/{id}", UpdateBlogItem).RequireAuthorization();
        blogItemGroup.MapDelete("/{id}", DeleteBlogItem).RequireAuthorization();
    }

    private static async Task<IResult> GetAllBlogItems(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllBlogItemsQuery(), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetBlogItemById(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetBlogItemByIdQuery(id), cancellationToken);

        return result is not null
            ? Results.Ok(result)
            : Results.NotFound(new { message = $"Blog item with id {id} not found" });
    }

    private static async Task<IResult> CreateBlogItem(
        [FromForm] CreateBlogItemCommand command,
        [FromForm] IFormFile image,
        [FromServices] IMediator mediator,
        [FromServices] IFileStorageService fileStorageService,
        CancellationToken cancellationToken)
    {
        if (image is null || image.Length == 0)
            return Results.BadRequest("Blog item cover image is not provided or empty.");

        var uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
        await using var stream = image.OpenReadStream();
        var fileUrl = await fileStorageService.UploadFileAsync(stream, uniqueFileName, image.ContentType);

        command = command with { CoverImgUrl = fileUrl };

        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/blog-items/{result}", new { id = result, message = "Blog item created successfully" });
    }

    private static async Task<IResult> UpdateBlogItem(
        [FromRoute] string id,
        [FromForm] UpdateBlogItemCommand command,
        [FromForm] IFormFile? image,
        [FromServices] IMediator mediator,
        [FromServices] IFileStorageService fileStorageService,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return Results.BadRequest(new { message = "Route id and command id do not match" });

        if (image is not null && image.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
            await using var stream = image.OpenReadStream();
            var fileUrl = await fileStorageService.UploadFileAsync(stream, uniqueFileName, image.ContentType);
            command = command with { CoverImgUrl = fileUrl };
        }

        await mediator.Send(command, cancellationToken);
        return Results.Ok(new { message = "Blog item updated successfully" });
    }

    private static async Task<IResult> DeleteBlogItem(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RemoveBlogItemCommand(id), cancellationToken);
        return Results.Ok(new { message = result });
    }
}
