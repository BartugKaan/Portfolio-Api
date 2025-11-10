using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Queries.GetAll;
using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Queries.GetById;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.WebApi.Endpoints.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

public class HeroEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var heroGroup = app.MapGroup("/api/hero")
            .WithTags("Hero")
            .WithOpenApi();

        heroGroup.MapGet("/", GetAllHeroes)
            .WithName("GetAllHeroes")
            .WithSummary("Get all hero entries")
            .Produces<IEnumerable<Hero>>(StatusCodes.Status200OK);

        heroGroup.MapGet("/{id}", GetHeroById)
            .WithName("GetHeroById")
            .WithSummary("Get hero entry by id")
            .Produces<Hero>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        heroGroup.MapPost("/", CreateHero)
            .WithName("CreateHero")
            .WithSummary("Create a new hero entry")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        heroGroup.MapPut("/{id}", UpdateHero)
            .WithName("UpdateHero")
            .WithSummary("Update an existing hero entry")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        heroGroup.MapDelete("/{id}", DeleteHero)
            .WithName("DeleteHero")
            .WithSummary("Delete hero by id")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetAllHeroes(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllHeroesQuery(), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetHeroById(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetHeroByIdQuery(id), cancellationToken);

        return result is not null
            ? Results.Ok(result)
            : Results.NotFound(new { message = $"Hero with id {id} not found" });
    }

    private static async Task<IResult> CreateHero(
        [FromBody] CreateHeroCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/hero/{result}", new { id = result, message = "Hero created successfully" });
    }

    private static async Task<IResult> UpdateHero(
        [FromRoute] string id,
        [FromBody] UpdateHeroCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return Results.BadRequest(new { message = "Route id and command id do not match" });

        await mediator.Send(command, cancellationToken);
        return Results.Ok(new { message = "Hero updated successfully" });
    }

    private static async Task<IResult> DeleteHero(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RemoveHeroCommand(id), cancellationToken);
        return Results.Ok(new { message = result });
    }
}
