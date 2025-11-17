using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Queries.GetAll;
using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Queries.GetById;
using BartugWeb.WebApi.Endpoints.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

public class SocialMediaEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var socialMediaGroup = app.MapGroup("/api/social-media")
            .WithTags("SocialMedia")
            .WithOpenApi();

        socialMediaGroup.MapGet("/", GetAllSocialMedia);
        socialMediaGroup.MapGet("/{id}", GetSocialMediaById);
        socialMediaGroup.MapPost("/", CreateSocialMedia).RequireAuthorization();
        socialMediaGroup.MapPut("/{id}", UpdateSocialMedia).RequireAuthorization();
        socialMediaGroup.MapDelete("/{id}", DeleteSocialMedia).RequireAuthorization();
    }

    private static async Task<IResult> GetAllSocialMedia(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllSocialMediaQuery(), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetSocialMediaById(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetSocialMediaByIdQuery(id), cancellationToken);

        return result is not null
            ? Results.Ok(result)
            : Results.NotFound(new { message = $"Social media with id {id} not found" });
    }

    private static async Task<IResult> CreateSocialMedia(
        [FromForm] CreateSocialMediaCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/social-media/{result}", new { id = result, message = "Social media created successfully" });
    }

    private static async Task<IResult> UpdateSocialMedia(
        [FromRoute] string id,
        [FromForm] UpdateSocialMediaCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return Results.BadRequest(new { message = "Route id and command id do not match" });

        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(new { message = "Social media updated successfully" });
    }

    private static async Task<IResult> DeleteSocialMedia(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RemoveSocialMediaCommand(id), cancellationToken);
        return Results.Ok(new { message = result });
    }
}
