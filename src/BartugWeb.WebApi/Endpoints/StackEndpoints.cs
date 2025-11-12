using BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.StackFeatures.Queries.GetByAboutId;
using BartugWeb.ApplicationLayer.Feature.StackFeatures.Queries.GetByCategory;
using BartugWeb.ApplicationLayer.Feature.StackFeatures.Queries.GetById;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.WebApi.Endpoints.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

/// <summary>
/// Stack feature endpoints
/// </summary>
public class StackEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var stackGroup = app.MapGroup("/api/stacks")
            .WithTags("Stacks")
            .WithOpenApi();

        stackGroup.MapGet("/about/{aboutId}", GetStacksByAboutId)
            .WithName("GetStacksByAboutId")
            .WithSummary("Get all stacks by about id (ordered by category and order)")
            .Produces<IEnumerable<Stack>>(StatusCodes.Status200OK);

        stackGroup.MapGet("/about/{aboutId}/category/{category}", GetStacksByCategory)
            .WithName("GetStacksByCategory")
            .WithSummary("Get stacks by about id and category")
            .Produces<IEnumerable<Stack>>(StatusCodes.Status200OK);

        stackGroup.MapGet("/{id}", GetStackById)
            .WithName("GetStackById")
            .WithSummary("Get stack by id")
            .Produces<Stack>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        stackGroup.MapPost("/", CreateStack)
            .WithName("CreateStack")
            .WithSummary("Create a new stack entry")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization();

        stackGroup.MapPut("/{id}", UpdateStack)
            .WithName("UpdateStack")
            .WithSummary("Update an existing stack entry")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();

        stackGroup.MapDelete("/{id}", DeleteStack)
            .WithName("DeleteStack")
            .WithSummary("Delete stack by ID")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
    }

    private static async Task<IResult> GetStacksByAboutId(
        [FromRoute] string aboutId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetStacksByAboutIdQuery(aboutId);
        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetStacksByCategory(
        [FromRoute] string aboutId,
        [FromRoute] string category,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetStacksByCategoryQuery(aboutId, category);
        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetStackById(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetStackByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);

        return result is not null
            ? Results.Ok(result)
            : Results.NotFound(new { message = $"Stack with id {id} not found" });
    }

    private static async Task<IResult> CreateStack(
        [FromBody] CreateStackCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/stacks/{result}", new { id = result, message = "Stack created successfully" });
    }

    private static async Task<IResult> UpdateStack(
        [FromRoute] string id,
        [FromBody] UpdateStackCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (id != command.StackId)
            return Results.BadRequest(new { message = "Route id and command id do not match" });

        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(new { message = "Stack updated successfully" });
    }

    private static async Task<IResult> DeleteStack(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new RemoveStackCommand(id);
        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(new { message = result });
    }
}
