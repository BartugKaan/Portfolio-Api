using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.PersistanceLayer.Context;
using BartugWeb.PersistanceLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BartugWeb.PersistanceLayer;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistanceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("PostgreSqlDefault"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorCodesToAdd: null);
                });
        });

        // Unit of Work Registration
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        // Repository Registrations
        services.AddScoped<IAboutRepository, AboutRepository>();
        services.AddScoped<IBlogItemRepository, BlogItemRepository>();
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<IGetInTouchRepository, GetInTouchRepository>();
        services.AddScoped<IHeroRepository, HeroRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
        services.AddScoped<IExperienceRepository, ExperienceRepository>();
        services.AddScoped<IEducationRepository, EducationRepository>();
        services.AddScoped<IStackRepository, StackRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();

        return services;
    }
}