using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors;

public class MethodInterception : MethodInterceptionBaseAttribute
{
    public override void Intercept(IInvocation invocation)
    {
        bool isSuccess = true;

        OnBefore(invocation);
        try
        {
            invocation.Proceed();
        }
        catch (Exception ex)
        {
            isSuccess = false;
            OnException(invocation, ex);
            throw;
        }
        finally
        {
            if (isSuccess)
                OnSuccess(invocation);
        }
        OnAfter(invocation);
    }

    public virtual void OnBefore(IInvocation invocation) { }
    public virtual void OnAfter(IInvocation invocation) { }
    public virtual void OnSuccess(IInvocation invocation) { }
    public virtual void OnException(IInvocation invocation, Exception exception) { }
}
