namespace BartugWeb.WebApi.Middlewares.Utils;

public class ValidationErrorDetails : ErrorStatusCode
{
    public IEnumerable<string> Errors { get; set; }
}