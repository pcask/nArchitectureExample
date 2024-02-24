using Core.Abstracts;
using Infrastructure.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistration
{
    public static void RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICheckIdentityService, CheckIdentityTRAdapter>();
        //services.AddKeyedScoped<ICheckIdentityService, CheckIdentityUSAAdapter>("USA");
    }
}
