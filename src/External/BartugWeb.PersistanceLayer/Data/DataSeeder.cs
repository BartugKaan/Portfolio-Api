using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BartugWeb.PersistanceLayer.Data;

public static class DataSeeder
{
    public static async Task SeedAdminUserAsync(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var adminRepository = scope.ServiceProvider.GetRequiredService<IAdminRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var admins = await adminRepository.GetAllAsync(default);
        if (admins.Any())
        {
            return;
        }

        var adminUsername = configuration["DefaultAdmin:Username"];
        var adminPassword = configuration["DefaultAdmin:Password"];

        if (string.IsNullOrEmpty(adminUsername) || string.IsNullOrEmpty(adminPassword))
        {
            throw new Exception("No admin users have been configured.");
        }

        var newAdmin = new Admin
        {
            Id = Guid.NewGuid().ToString(),
            Username = adminUsername,
            PasswordHash = passwordHasher.Hash(adminPassword),
            Email = "bartugkaan@mail.com"
        };

        await adminRepository.AddAsync(newAdmin, default);
        
        await unitOfWork.SaveChangesAsync(default);
    }
}