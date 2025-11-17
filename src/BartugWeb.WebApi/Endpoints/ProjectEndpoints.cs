using BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Queries.GetAll;
using BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Queries.GetById;
using BartugWeb.WebApi.Endpoints.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

public class ProjectEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var projectGroup = app.MapGroup("/api/projects")
            .WithTags("Projects")
            .WithOpenApi();

        projectGroup.MapGet("/", GetAllProjects)
            .WithName("GetAllProjects")
            .WithSummary("Get all projects");

        projectGroup.MapGet("/{id}", GetProjectById)
            .WithName("GetProjectById")
            .WithSummary("Get project by id");

        projectGroup.MapPost("/", CreateProject)
            .WithName("CreateProject")
            .WithSummary("Create a new project")
            .RequireAuthorization();

        projectGroup.MapPut("/{id}", UpdateProject)
            .WithName("UpdateProject")
            .WithSummary("Update an existing project")
            .RequireAuthorization();

        projectGroup.MapDelete("/{id}", DeleteProject)
            .WithName("DeleteProject")
            .WithSummary("Delete project by id")
            .RequireAuthorization();
    }

    private static async Task<IResult> GetAllProjects(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllProjectsQuery(), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetProjectById(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProjectByIdQuery(id), cancellationToken);

        return result is not null
            ? Results.Ok(result)
            : Results.NotFound(new { message = $"Project with id {id} not found" });
    }

    private static async Task<IResult> CreateProject(
        [FromForm] CreateProjectCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/projects/{result}", new { id = result, message = "Project created successfully" });
    }

    private static async Task<IResult> UpdateProject(
        [FromRoute] string id,
        [FromForm] UpdateProjectCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return Results.BadRequest(new { message = "Route id and command id do not match" });
        
        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(new { message = "Project updated successfully" });
    }

    private static async Task<IResult> DeleteProject(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RemoveProjectCommand(id), cancellationToken);
        return Results.Ok(new { message = result });
    }
}
