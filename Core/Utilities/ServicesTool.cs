using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public class ServicesTool
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static void CreateServiceProvider(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
        }

        public static T GetService<T>() => ServiceProvider.GetService<T>() ?? throw new Exception("Service not found!");
    }
}
