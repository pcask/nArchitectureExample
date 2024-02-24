using Castle.DynamicProxy;
using Core.Utilities;
using Core.Utilities.Interceptors;
using System.Diagnostics;

namespace Core.Aspects.Autofac.Performance;

public class PerformanceAspect : MethodInterception
{
    private readonly int _interval;
    private readonly Stopwatch _stopwatch;

    public PerformanceAspect(int interval)
    {
        _interval = interval;
        _stopwatch = ServicesTool.GetService<Stopwatch>();
    }

    public override void OnBefore(IInvocation invocation)
    {
        _stopwatch.Start();
    }

    public override void OnAfter(IInvocation invocation)
    {
        var elapsedSec = _stopwatch.Elapsed.TotalSeconds;
        if (elapsedSec > _interval)
        {
            Debug.Write($"\n[ Performance ] The total running times of '{invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}' is {elapsedSec} seconds.!\n");
        }
        _stopwatch.Stop();
        _stopwatch.Reset();
    }
}

