﻿using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors;

[AttributeUsage(
    AttributeTargets.Class |
    AttributeTargets.Method |
    AttributeTargets.Assembly,
    AllowMultiple = true,
    Inherited = true)]
public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
{
    public short Priority { get; set; }
    public virtual void Intercept(IInvocation invocation)
    {
    }
}
