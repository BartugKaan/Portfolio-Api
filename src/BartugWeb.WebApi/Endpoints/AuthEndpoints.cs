using BartugWeb.ApplicationLayer.Feature.AuthFeatures.Commands;
using BartugWeb.ApplicationLayer.Feature.AuthFeatures.Commands.CreateAdmin;
using BartugWeb.ApplicationLayer.Feature.AuthFeatures.DTOs;
using BartugWeb.WebApi.Endpoints.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

public class AuthEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var auth = app.MapGroup("/api/auth")
            .WithTags("Authentication")
            .WithOpenApi();

        auth.MapPost("/login", Login)
            .WithName("AdminLogin")
            .WithSummary("Admin Login endpoint.")
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        auth.MapPost("/register-admin", RegisterAdmin)
            .WithName("RegisterFirstAdmin")
            .WithSummary("Create the first admin user. Should be removed after first use.")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var command = new LoginCommand(request.Username, request.Password);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Results.Unauthorized();
        }
    }

    private static async Task<IResult> RegisterAdmin(
        [FromBody] CreateAdminCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(command, cancellationToken);
            return Results.Created("/api/auth/login", result);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { message = ex.Message });
        }
    }
}
