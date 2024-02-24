using Business.Abstracts;
using Business.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Business;

public static class ServiceRegistration
{
    public static void RegisterBusinessServices(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, ConsoleLoggerManager>();

        Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && t.Name.EndsWith("Validations"))
                .ToList()
                .ForEach(t => services.AddScoped(t));
    }
}
