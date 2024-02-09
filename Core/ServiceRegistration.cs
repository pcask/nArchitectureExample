using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceRegistration
{
    public static void RegisterCoreServices(this IServiceCollection services)
    {
        //services.AddSingleton<ITokenHelper, JWTTokenHelper>();
    }
}
