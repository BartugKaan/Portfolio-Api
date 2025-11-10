using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.RemoveCommands;
using BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.CreateCommand;
using BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.UpdateCommand;
using BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Queries.GetAll;
using BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Queries.GetById;
using BartugWeb.WebApi.Endpoints.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

public class BlogPostEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var blogPostGroup = app.MapGroup("/api/blog-posts")
            .WithTags("BlogPosts")
            .WithOpenApi();
    }

    private static async Task<IResult> GetAllBlogPosts(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetAllBlogPostsQuery();
        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetBlogPostById(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetBlogPostByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return result is not null ? Results.Ok(result) : Results.NotFound();
    }

    private static async Task<IResult> CreateBlogPost(
        [FromBody] CreateBlogPostCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/blog-posts/{result}",
            new { id = result, message = "Blog Post created successfully" });
    }

    private static async Task<IResult> UpdateBlogPost(
        [FromRoute] string id,
        [FromBody] UpdateBlogPostCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if(id != command.Id)
            return Results.BadRequest(new {message="Route id and command id do not match"});
        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(new {message = "Blog Post updated successfully"});
    }

    private static async Task<IResult> DeleteBlogPost(
        [FromRoute] string id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
        {
            var command = new RemoveAboutCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            return Results.Ok(new {message = "Blog Post removed successfully"});
        }
}