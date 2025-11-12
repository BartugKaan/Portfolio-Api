using BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.CreateCommand;
using BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Queries.GetByAboutId;
using BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Queries.GetById;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.WebApi.Endpoints.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

/// <summary>
/// Experience feature endpoints
/// </summary>
public class ExperienceEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var experienceGroup = app.MapGroup("/api/experiences")
            .WithTags("Experiences")
            .WithOpenApi();

        experienceGroup.MapGet("/about/{aboutId}", GetExperiencesByAboutId)
            .WithName("GetExperiencesByAboutId")
            .WithSummary("Get all experiences by about id")
            .Produces<IEnumerable<Experience>>(StatusCodes.Status200OK);

        experienceGroup.MapGet("/{id}", GetExperienceById)
            .WithName("GetExperienceById")
            .WithSummary("Get experience by id")
            .Produces<Experience>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        experienceGroup.MapPost("/", CreateExperience)
            .WithName("CreateExperience")
            .WithSummary("Create a new experience")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization();

        experienceGroup.MapPut("/{id}", UpdateExperience)
            .WithName("UpdateExperience")
            .WithSummary("Update an existing experience")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();

        experienceGroup.MapDelete("/{id}", DeleteExperience)
            .WithName("DeleteExperience")
            .WithSummary("Delete experience by ID")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
    }

    private static async Task<IResult> GetExperiencesByAboutId(
        [FromRoute] string aboutId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetExperiencesByAboutIdQuery(aboutId);
        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetExperienceById(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetExperienceByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);

        return result is not null
            ? Results.Ok(result)
            : Results.NotFound(new { message = $"Experience with id {id} not found" });
    }

    private static async Task<IResult> CreateExperience(
        [FromBody] CreateExperienceCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/experiences/{result}", new { id = result, message = "Experience created successfully" });
    }

    private static async Task<IResult> UpdateExperience(
        [FromRoute] string id,
        [FromBody] UpdateExperienceCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (id != command.ExperienceId)
            return Results.BadRequest(new { message = "Route id and command id do not match" });

        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(new { message = "Experience updated successfully" });
    }

    private static async Task<IResult> DeleteExperience(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new RemoveExperienceCommand(id);
        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(new { message = result });
    }
}
