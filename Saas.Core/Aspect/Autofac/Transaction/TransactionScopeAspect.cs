using System.Transactions;
using Castle.DynamicProxy;
using Saas.Core.Utilities.Interceptors;

namespace Saas.Core.Aspect.Autofac.Transaction
{
    public class TransactionScopeAspect :MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            TransactionScope transactionScope = new TransactionScope();
            try
            {
                invocation.Proceed();
                transactionScope.Complete();

            }
            catch (TransactionException exception)
            {
                throw exception;
            }
            finally
            {
                transactionScope.Dispose();
            }
        }
    }
}
