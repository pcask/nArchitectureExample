using DataAccess.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DataAccess;

public static class ServiceRegistration
{
    public static void RegisterDataAccessServices(this IServiceCollection services)
    {
        services.AddDbContext<NADbContext>();

        Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && t.Name.EndsWith("Repository"))
                .ToList()
                .ForEach(implementationType =>
                {
                    var serviceType = implementationType.GetInterfaces().FirstOrDefault(st => st.Name.EndsWith(implementationType.Name));

                    if (serviceType != null)
                        services.AddScoped(serviceType, implementationType);
                });
    }
}
