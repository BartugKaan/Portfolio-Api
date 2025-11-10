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

        blogItemGroup.MapGet("/", GetAllBlogItems)
            .WithName("GetAllBlogItems")
            .WithSummary("Get all blog items")
            .Produces<IEnumerable<BlogItem>>(StatusCodes.Status200OK);

        blogItemGroup.MapGet("/{id}", GetBlogItemById)
            .WithName("GetBlogItemById")
            .WithSummary("Get blog item by id")
            .Produces<BlogItem>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        blogItemGroup.MapPost("/", CreateBlogItem)
            .WithName("CreateBlogItem")
            .WithSummary("Create a new blog item")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        blogItemGroup.MapPut("/{id}", UpdateBlogItem)
            .WithName("UpdateBlogItem")
            .WithSummary("Update an existing blog item")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        blogItemGroup.MapDelete("/{id}", DeleteBlogItem)
            .WithName("DeleteBlogItem")
            .WithSummary("Delete blog item by id")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
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
        [FromBody] CreateBlogItemCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/blog-items/{result}", new { id = result, message = "Blog item created successfully" });
    }

    private static async Task<IResult> UpdateBlogItem(
        [FromRoute] string id,
        [FromBody] UpdateBlogItemCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return Results.BadRequest(new { message = "Route id and command id do not match" });

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
