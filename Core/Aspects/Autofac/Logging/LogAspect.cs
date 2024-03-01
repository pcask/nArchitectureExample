using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System.Diagnostics;

namespace Core.Aspects.Autofac.Logging;

public class LogAspect : MethodInterception
{
    public override void OnBefore(IInvocation invocation)
    {
        Debug.Write($"Before calling the '{invocation.Method.DeclaringType.FullName}' method");
    }

    public override void OnException(IInvocation invocation, Exception exception)
    {
        Debug.Write($"\nAn exception occurred while processing '{invocation.Method.DeclaringType.FullName}' method. \n[EXCEPTION] = {exception.Message} ");
    }

    public override void OnAfter(IInvocation invocation)
    {
        Debug.Write($"After calling the '{invocation.Method.DeclaringType.FullName}' method");
    }
}
