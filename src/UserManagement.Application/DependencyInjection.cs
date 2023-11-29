using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Configurations;

namespace UserManagement.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.ResolveDependencies(builder);
        services.AddAuthConfiguration(builder);
        services.AddCorsConfig();
        services.Configure<ApiBehaviorOptions>(o => o.SuppressModelStateInvalidFilter = true);
    }
}