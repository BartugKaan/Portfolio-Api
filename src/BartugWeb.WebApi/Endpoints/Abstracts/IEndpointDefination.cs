namespace BartugWeb.WebApi.Endpoints.Abstracts;

/// <summary>
/// Defines a contract for endpoint definitions in the web application.
/// </summary>
public interface IEndpointDefination
{
    void DefineEndpoints(WebApplication app);
}