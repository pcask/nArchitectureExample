using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities;
using Core.Utilities.Interceptors;
using System.Text.Json;

namespace Core.Aspects.Autofac.Caching;

public class CacheAspect : MethodInterception
{
    private readonly ICacheService _cacheService;
    private readonly int _durationMinute;

    public CacheAspect(int durationMinute)
    {
        _cacheService = ServicesTool.GetService<ICacheService>();
        _durationMinute = durationMinute;
    }

    public override void Intercept(IInvocation invocation)
    {
        string key = invocation.Method.DeclaringType?.FullName + "." + invocation.Method.Name;

        var args = invocation.Arguments;
        var _params = invocation.Method.GetParameters();
        if (args.Length > 0)
        {
            string[] _arguments = new string[args.Length];

            for (int i = 0; i < args.Length; i++)
            {
                _arguments[i] = _params[i].Name + "=" + JsonSerializer.Serialize(args[i]);
            }

            key += "|" + string.Join("|", _arguments);
        }

        if (_cacheService.IsAdd(key))
        {
            invocation.ReturnValue = _cacheService.Get(key);
            return;
        }
        invocation.Proceed();
        _cacheService.Add(key, invocation.ReturnValue, _durationMinute);
    }
}
