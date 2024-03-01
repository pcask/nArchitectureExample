using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Validation;
using Core.Security.JWT;
using Core.Utilities.ByPass;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core;

public static class ServiceRegistration
{
    public static void RegisterCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<ITokenHelper, JWTTokenHelper>();

        services.AddHttpContextAccessor();
        services.AddSingleton<Stopwatch>();
        services.AddMemoryCache();
        services.AddScoped<AuthorizationByPass>();
        services.AddScoped<ValidationReturn>();

        services.AddSingleton<ICacheService, MicrosoftCacheManager>();
    }
}
