using Core.Abstracts;
using Infrastructure.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistration
{
    public static void RegisterInfrastructureServices(this IServiceCollection services)
    {
        //services.AddKeyedScoped<ICheckIdentityService, CheckIdentityAdapterTR>("TR");
        //services.AddKeyedScoped<ICheckIdentityService, CheckIdentityAdapterUSA>("USA");
    }
}
