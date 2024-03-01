using Castle.DynamicProxy;
using Core.Exceptions;
using Core.Utilities;
using Core.Utilities.ByPass;
using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Core.Aspects.Autofac.Security;

public class SecurityAspect : MethodInterception
{
    private readonly HttpContext httpContext;
    private readonly AuthorizationByPass authorizationByPass;

    private string[] ExternalRoles { get; set; }

    public SecurityAspect(string[] externalRoles = null)
    {
        Priority = -1;
        ExternalRoles = externalRoles ?? [];

        httpContext = ServicesTool.GetService<IHttpContextAccessor>()?.HttpContext;
        authorizationByPass = ServicesTool.GetService<AuthorizationByPass>();
    }
    public override void OnBefore(IInvocation invocation)
    {
        // ByPass with flag
        if (authorizationByPass.ByPass)
        {
            authorizationByPass.ByPass = false;
            return;
        }

        // ByPass with attribute
        //if (invocation.Method.GetCustomAttributes(typeof(WithoutAuthorization), true).Length > 0)
        //    return;

        var roles = ExternalRoles.ToList();

        string requiredRole = $"{invocation.Method.ReflectedType.Name}.{invocation.Method.Name}";
        roles.Add(requiredRole);
        roles.Add("IT.supervisor");

        var user = httpContext?.User;

        if (user?.Identity?.IsAuthenticated == false)
            throw new AuthenticationException("You are not authenticated!");

        var isAuthorized = user.Claims.Any(c => c.Type == ClaimTypes.Role && roles.Contains(c.Value));
        if (!isAuthorized)
            throw new AuthorizationException(requiredRole, "You are not authorized!");
    }
}
