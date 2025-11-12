using BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.EducationFeatures.Queries.GetByAboutId;
using BartugWeb.ApplicationLayer.Feature.EducationFeatures.Queries.GetById;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.WebApi.Endpoints.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

/// <summary>
/// Education feature endpoints
/// </summary>
public class EducationEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var educationGroup = app.MapGroup("/api/educations")
            .WithTags("Educations")
            .WithOpenApi();

        educationGroup.MapGet("/about/{aboutId}", GetEducationsByAboutId)
            .WithName("GetEducationsByAboutId")
            .WithSummary("Get all educations by about id")
            .Produces<IEnumerable<Education>>(StatusCodes.Status200OK);

        educationGroup.MapGet("/{id}", GetEducationById)
            .WithName("GetEducationById")
            .WithSummary("Get education by id")
            .Produces<Education>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        educationGroup.MapPost("/", CreateEducation)
            .WithName("CreateEducation")
            .WithSummary("Create a new education")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization();

        educationGroup.MapPut("/{id}", UpdateEducation)
            .WithName("UpdateEducation")
            .WithSummary("Update an existing education")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();

        educationGroup.MapDelete("/{id}", DeleteEducation)
            .WithName("DeleteEducation")
            .WithSummary("Delete education by ID")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
    }

    private static async Task<IResult> GetEducationsByAboutId(
        [FromRoute] string aboutId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetEducationsByAboutIdQuery(aboutId);
        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetEducationById(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetEducationByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);

        return result is not null
            ? Results.Ok(result)
            : Results.NotFound(new { message = $"Education with id {id} not found" });
    }

    private static async Task<IResult> CreateEducation(
        [FromBody] CreateEducationCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/educations/{result}", new { id = result, message = "Education created successfully" });
    }

    private static async Task<IResult> UpdateEducation(
        [FromRoute] string id,
        [FromBody] UpdateEducationCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (id != command.EducationId)
            return Results.BadRequest(new { message = "Route id and command id do not match" });

        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(new { message = "Education updated successfully" });
    }

    private static async Task<IResult> DeleteEducation(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new RemoveEducationCommand(id);
        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(new { message = result });
    }
}
