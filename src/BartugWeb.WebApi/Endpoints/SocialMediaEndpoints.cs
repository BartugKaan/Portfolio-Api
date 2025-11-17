using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Queries.GetAll;
using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Queries.GetById;
using BartugWeb.DomainLayer.Entities;
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
        [FromForm] IFormFile icon,
        [FromServices] IMediator mediator,
        [FromServices] IFileStorageService fileStorageService,
        CancellationToken cancellationToken)
    {
        if (icon is null || icon.Length == 0)
            return Results.BadRequest("Social media icon is not provided or empty.");

        var uniqueFileName = $"{Guid.NewGuid()}_{icon.FileName}";
        await using var stream = icon.OpenReadStream();
        var fileUrl = await fileStorageService.UploadFileAsync(stream, uniqueFileName, icon.ContentType);

        command = command with { IconUrl = fileUrl };

        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/social-media/{result}", new { id = result, message = "Social media created successfully" });
    }

    private static async Task<IResult> UpdateSocialMedia(
        [FromRoute] string id,
        [FromForm] UpdateSocialMediaCommand command,
        [FromForm] IFormFile? icon,
        [FromServices] IMediator mediator,
        [FromServices] IFileStorageService fileStorageService,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return Results.BadRequest(new { message = "Route id and command id do not match" });

        if (icon is not null && icon.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{icon.FileName}";
            await using var stream = icon.OpenReadStream();
            var fileUrl = await fileStorageService.UploadFileAsync(stream, uniqueFileName, icon.ContentType);
            command = command with { IconUrl = fileUrl };
        }

        await mediator.Send(command, cancellationToken);
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
