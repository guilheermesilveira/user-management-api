using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ScottBrady91.AspNetCore.Identity;
using UserManagement.Application.Contracts;
using UserManagement.Application.Notifications;
using UserManagement.Application.Services;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Configurations;

public static class DependencyConfig
{
    public static void ResolveDependencies(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services
            .AddSingleton(_ => builder.Configuration);
        
        services
            .AddScoped<INotificator, Notificator>()
            .AddScoped<IPasswordHasher<User>, Argon2PasswordHasher<User>>();

        services
            .AddScoped<IAdministratorService, AdministratorService>()
            .AddScoped<IUserService, UserService>();
    }
}