// src/BartugWeb.WebApi/Endpoints/AuthEndpoints.cs

using BartugWeb.ApplicationLayer.Feature.AuthFeatures.Commands;
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
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest); // Hata middleware'i yakalayacağı için eklendi
    }

    private static async Task<IResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var command = new LoginCommand(request.Username, request.Password);
        var response = await mediator.Send(command, cancellationToken);
        return Results.Ok(response);
    }
}