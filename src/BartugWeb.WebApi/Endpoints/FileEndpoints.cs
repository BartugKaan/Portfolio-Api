using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.WebApi.Endpoints.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Endpoints;

public class FileEndpoints : IEndpointDefination
{
    public void DefineEndpoints(WebApplication app)
    {
        var files = app.MapGroup("/api/files")
            .WithTags("Files")
            .WithOpenApi();

        files.MapPost("/upload", UploadFile)
            .WithName("UploadFile")
            .WithSummary("Uploads a file to the storage")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization();

        files.MapDelete("/{fileName}", DeleteFile)
            .WithName("DeleteFile")
            .WithSummary("Deletes a file from the storage")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
    }

    private static async Task<IResult> UploadFile(
        [FromForm] IFormFile file,
        [FromServices] IFileStorageService fileStorageService,
        HttpContext httpContext)
    {
        if (file is null || file.Length == 0)
            return Results.BadRequest("File is not provided or Empty.");
        
        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        await using var stream = file.OpenReadStream();
        var fileUrl = await fileStorageService.UploadFileAsync(stream, uniqueFileName, file.ContentType);

        return Results.Created(fileUrl, new { Url = fileUrl });
    }

    private static async Task<IResult> DeleteFile(
        [FromRoute] string fileName,
        [FromServices] IFileStorageService fileStorageService)
    {
        try
        {
            await fileStorageService.DeleteFileAsync(fileName);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.NotFound($"File {fileName} not found or could not be deleted. Error: {e.Message}");
        }
    }
}