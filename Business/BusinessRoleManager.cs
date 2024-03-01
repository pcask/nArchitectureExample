using Core.CrossCuttingConcerns.Security;
using Core.Utilities;
using DataAccess.Abstracts;
using System.Reflection;

namespace Core;

public static class BusinessRoleManager
{
    private static readonly IClaimRepository claimRepository = ServicesTool.GetService<IClaimRepository>();

    public static void RegisterBusinessRoles()
    {
        Assembly.GetExecutingAssembly()
                .GetTypes().Where(t => t.IsClass && t.GetCustomAttribute<MustBeAuthorized>() != null).ToList()
                .ForEach(t => t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Where(m => !m.Name.StartsWith("get_") && !m.Name.StartsWith("set_"))
                .ToList()
                .ForEach(m =>
                {
                    var implementedInterface = t.GetInterfaces().FirstOrDefault(i => i.Name.EndsWith("Service"))?.Name ?? t.Name;

                    var claim = claimRepository.Get(c => c.Group == implementedInterface && c.Name == m.Name);
                    if (claim == null)
                        claimRepository.Add(new() { Group = implementedInterface, Name = m.Name });
                }));
    }
}
