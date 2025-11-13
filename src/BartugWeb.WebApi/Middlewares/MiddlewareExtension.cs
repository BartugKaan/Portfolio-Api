// src/BartugWeb.WebApi/Middlewares/MiddlewareExtension.cs

using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BartugWeb.WebApi.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = GetTitle(exception),
            Detail = exception.Message,
            Extensions = GetErrors(exception)
        };

        var json = JsonSerializer.Serialize(problemDetails);
        await context.Response.WriteAsync(json);
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            InvalidOperationException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ValidationException => "Validation Error",
            InvalidOperationException => "Invalid Operation",
            UnauthorizedAccessException => "Unauthorized",
            _ => "Server Error"
        };

    private static IDictionary<string, object?>? GetErrors(Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            return validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => (object?)g.Select(e => e.ErrorMessage).ToArray()
                );
        }

        return null;
    }
}
