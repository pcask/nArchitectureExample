using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System.Reflection;

namespace Core.DependencyResolvers.Autofac;

public static class AutofacHelper
{
    internal static void RegisterByEndName(ContainerBuilder builder, Assembly assembly, string endName, ServiceLifeTime serviceLifeTime = ServiceLifeTime.InstancePerLifetimeScope, bool hasInterface = true)
    {
        var registrationBuilder = builder.RegisterAssemblyTypes(assembly)
                 .Where(x => x.Name.EndsWith(endName));

        if (hasInterface)
            registrationBuilder = registrationBuilder
                                  .AsImplementedInterfaces()
                                  .EnableInterfaceInterceptors(new ProxyGenerationOptions { Selector = new AspectInterceptorSelector() });


        _ = serviceLifeTime switch
        {
            ServiceLifeTime.SingleInstance => registrationBuilder.SingleInstance(),
            ServiceLifeTime.InstancePerLifetimeScope => registrationBuilder.InstancePerLifetimeScope(),
            ServiceLifeTime.InstancePerMatchingLifetimeScope => registrationBuilder.InstancePerMatchingLifetimeScope(),
            ServiceLifeTime.InstancePerDependency => registrationBuilder.InstancePerDependency(),
            ServiceLifeTime.InstancePerRequest => registrationBuilder.InstancePerRequest(),
            _ => registrationBuilder.SingleInstance()
        };

    }

    public enum ServiceLifeTime
    {
        SingleInstance,
        InstancePerLifetimeScope,
        InstancePerMatchingLifetimeScope,
        InstancePerDependency,
        InstancePerRequest
    }
}
