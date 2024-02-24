using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction;

public class TransactionScopeAspect : MethodInterception
{
    public override void Intercept(IInvocation invocation)
    {
        using (var transaction = new TransactionScope())
        {
            try
            {
                invocation.Proceed();
                transaction.Complete();
            }
            catch (Exception ex)
            {
                transaction.Dispose();
                throw;
            }
        }
    }
}
