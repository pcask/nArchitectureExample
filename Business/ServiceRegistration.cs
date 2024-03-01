using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core;

public static class ServiceRegistration
{
    public static void RegisterBusinessServices(this IServiceCollection services)
    {
        Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && t.Name.EndsWith("Validations"))
                .ToList()
                .ForEach(t => services.AddScoped(t));
    }
}
