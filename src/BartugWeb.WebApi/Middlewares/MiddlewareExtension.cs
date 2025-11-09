using BartugWeb.WebApi.Middlewares.Utils;
using FluentValidation;

namespace BartugWeb.WebApi.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        throw new NotImplementedException();
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        if (exception.GetType() == typeof(ValidationException))
        {
            return context.Response.WriteAsync(new ValidationErrorDetails
            {
                Errors = ((ValidationException)exception).Errors.Select(e => e.PropertyName),
                StatusCode = 403
            }.ToString());
        }

        return context.Response.WriteAsync(new ErrorResult
        {
            Message = exception.Message,
            StatusCode = context.Response.StatusCode
        }.ToString());
    }
}