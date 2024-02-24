using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core;

public static class ServiceRegistration
{
    public static void RegisterCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<ITokenHelper, JWTTokenHelper>();

        services.AddHttpContextAccessor(); // Bu sayade istediğimiz katmandan HttpContext'e erişim sağlıyabiliriz.
        services.AddSingleton<Stopwatch>();
        services.AddMemoryCache();

        services.AddSingleton<ICacheService, MicrosoftCacheManager>();
    }
}
