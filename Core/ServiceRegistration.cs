using Core.Abstracts;
using Core.Adapters;
using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceRegistration
{
    public static void RegisterCoreServices(this IServiceCollection services)
    {
        services.AddKeyedScoped<ICheckIdentityService, CheckIdentityAdapterTR>("TR");
        services.AddKeyedScoped<ICheckIdentityService, CheckIdentityAdapterUSA>("USA");
        services.AddSingleton<ITokenHelper, JWTTokenHelper>();
    }
}
