using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Queries.GetAll;
using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Queries.GetById;
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

        heroGroup.MapGet("/", GetAllHeroes);
        heroGroup.MapGet("/{id}", GetHeroById);
        heroGroup.MapPost("/", CreateHero).RequireAuthorization();
        heroGroup.MapPut("/{id}", UpdateHero).RequireAuthorization();
        heroGroup.MapDelete("/{id}", DeleteHero).RequireAuthorization();
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
        [FromForm] CreateHeroCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/hero/{result}", new { id = result, message = "Hero created successfully" });
    }

    private static async Task<IResult> UpdateHero(
        [FromRoute] string id,
        [FromForm] UpdateHeroCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return Results.BadRequest(new { message = "Route id and command id do not match" });

        var result = await mediator.Send(command, cancellationToken);
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
