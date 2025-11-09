using System.Reflection;
using BartugWeb.WebApi.Endpoints.Abstracts;

namespace BartugWeb.WebApi.Extensions;

public static class EndpointExtensions
{
    public static void MapEndpointDefinitions(this WebApplication app)
    {
        var endpointDefinitions = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IEndpointDefination).IsAssignableFrom(t))
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefination>();

        foreach (var endpoint in endpointDefinitions)
        {
            endpoint.DefineEndpoints(app);
        }
    }
}