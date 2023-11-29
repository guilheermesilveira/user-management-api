using Microsoft.Extensions.DependencyInjection;
using UserManagement.Domain.Contracts.Repositories;
using UserManagement.Infra.Data.Repositories;

namespace UserManagement.Infra.Data.Configuration;

public static class DependencyConfig
{
    public static void ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>(); 
    }
}