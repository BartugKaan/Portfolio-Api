using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Queries.GetAll;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Queries.GetById;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.WebApi.Endpoints.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

/// <summary>
/// About feature endpoints
/// </summary>
public class AboutEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var aboutGroup = app.MapGroup("/api/about")
            .WithTags("About")
            .WithOpenApi();

        aboutGroup.MapGet("/", GetAllAbouts)
            .WithName("GetAllAbouts")
            .WithSummary("Get all about entries")
            .Produces<IEnumerable<About>>(StatusCodes.Status200OK);
        
        aboutGroup.MapGet("/{id}", GetAboutById)
            .WithName("GetAboutById")
            .WithSummary("Get about entry by id")
            .Produces<About>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        aboutGroup.MapPost("/", CreateAbout)
            .WithName("CreateAbout")
            .WithSummary("Create a new about entry")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization();
        
        aboutGroup.MapPut("/{id}", UpdateAbout)
            .WithName("UpdateAbout")
            .WithSummary("Update an existing about entry")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
        
        aboutGroup.MapDelete("/{id}", DeleteAbout)
            .WithName("DeleteAbout")
            .WithSummary("Delete about by ID")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
    }

    private static async Task<IResult> GetAllAbouts(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetAllAboutsQuery();
        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetAboutById(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetAboutByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        
        return result is not null
            ? Results.Ok(result)
            : Results.NotFound(new {message  = $"About with id {id} not found"});
    }

    private static async Task<IResult> CreateAbout(
        [FromBody] CreateAboutCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/about/{result}", new {id = result, message = "About created successfully"});
    }

    private static async Task<IResult> UpdateAbout(
        [FromRoute] string id,
        [FromBody] UpdateAboutCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if(id != command.AboutId)
            return Results.BadRequest(new {message="Route id and command id do not match"});
        
        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(new {message = "About updated successfully"});
        
    }
    
    private static async Task<IResult> DeleteAbout(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new RemoveAboutCommand(id);
        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(new {message = result});
    }
    
}