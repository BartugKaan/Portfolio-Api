using System;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.InfrastructureLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BartugWeb.InfrastructureLayer;

public static class ServiceRegistration
{
  public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
  {
    // JWT Services
    services.AddScoped<IJwtService, JwtService>();
    services.AddScoped<IPasswordHasher, PasswordHasher>();
    services.AddScoped<IFileStorageService, FileStorageService>();

    return services;
  }
}
