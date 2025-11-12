using BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Queries.GetAll;
using BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Queries.GetById;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.WebApi.Endpoints.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

public class GetInTouchEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var getInTouchGroup = app.MapGroup("/api/get-in-touch")
            .WithTags("GetInTouch")
            .WithOpenApi();

        getInTouchGroup.MapGet("/", GetAllGetInTouch)
            .WithName("GetAllGetInTouch")
            .WithSummary("Get all get-in-touch entries")
            .Produces<IEnumerable<GetInTouch>>(StatusCodes.Status200OK)
            .RequireAuthorization();

        getInTouchGroup.MapGet("/{id}", GetGetInTouchById)
            .WithName("GetGetInTouchById")
            .WithSummary("Get get-in-touch entry by id")
            .Produces<GetInTouch>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();

        getInTouchGroup.MapPost("/", CreateGetInTouch)
            .WithName("CreateGetInTouch")
            .WithSummary("Create a new get-in-touch entry")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        getInTouchGroup.MapPut("/{id}", UpdateGetInTouch)
            .WithName("UpdateGetInTouch")
            .WithSummary("Update an existing get-in-touch entry")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();

        getInTouchGroup.MapDelete("/{id}", DeleteGetInTouch)
            .WithName("DeleteGetInTouch")
            .WithSummary("Delete get-in-touch entry by id")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
    }

    private static async Task<IResult> GetAllGetInTouch(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllGetInTouchQuery(), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetGetInTouchById(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetGetInTouchByIdQuery(id), cancellationToken);

        return result is not null
            ? Results.Ok(result)
            : Results.NotFound(new { message = $"GetInTouch with id {id} not found" });
    }

    private static async Task<IResult> CreateGetInTouch(
        [FromBody] CreateGetInTouchCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/get-in-touch/{result}", new { id = result, message = "GetInTouch created successfully" });
    }

    private static async Task<IResult> UpdateGetInTouch(
        [FromRoute] string id,
        [FromBody] UpdateGetInTouchCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return Results.BadRequest(new { message = "Route id and command id do not match" });

        await mediator.Send(command, cancellationToken);
        return Results.Ok(new { message = "GetInTouch updated successfully" });
    }

    private static async Task<IResult> DeleteGetInTouch(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RemoveGetInTouchCommand(id), cancellationToken);
        return Results.Ok(new { message = result });
    }
}
