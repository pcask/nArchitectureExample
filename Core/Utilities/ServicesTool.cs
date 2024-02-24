using Autofac;
using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities;

public class ServicesTool
{
    public static IServiceProvider ServiceProvider { get; set; }
    private static ILifetimeScope _lifetimeScope { get; set; } // From Autofac

    public static void CreateServiceProvider(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
    }

    public static void CreateAutofacServiceProvider(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope.BeginLifetimeScope();
    }

    public static T GetService<T>() => ServiceProvider.GetService<T>() ?? throw new Exception("Service not found!");
    public static object GetService(Type type) => ServiceProvider.GetService(type) ?? throw new Exception("Service not found");
    public static T GetAutofacService<T>()
    {
        return _lifetimeScope.Resolve<T>() ?? throw new Exception("Service not found!");
    }
    public static object GetAutofacService(Type type)
    {
        if (_lifetimeScope.TryResolve(type, out object instance))
            return instance;

        throw new Exception("Service not found in Autofac Container!");
    }

    //public static object GetDependentAutofacServiceWithKey(Type type, (string key, Type type) dependency)
    //{
    //    return _lifetimeScope.Resolve(type, new NamedParameter(dependency.key.ToString(), Activator.CreateInstance(dependency.type)))
    //        ?? throw new Exception("Service not found in Autofac Container!");
    //}

    public static IInstanceActivator GetAutofacServiceByName(string serviceName)
    {
        return _lifetimeScope.ComponentRegistry.Registrations
            .FirstOrDefault(s => s.Services.First().Description == serviceName)?
            .Activator ?? throw new Exception("Service not found in Autofac Container!");
    }
}