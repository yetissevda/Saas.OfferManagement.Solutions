using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Saas.Core.CrossCuttingConcerns.Logging;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net;
using Saas.Core.Utilities.Interceptors;
using Saas.Core.Utilities.Messages;

namespace Saas.Core.Aspect.Autofac.Exception
{
    public class ExceptionLogAspect :MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;

        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLogType);
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }
        protected override void OnException(IInvocation invocation,System.Exception e)
        {
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = e.Message;
            _loggerServiceBase.Error(logDetailWithException);
        }

        private static LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logparameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logparameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var logDetailWithException = new LogDetailWithException
            {
                MethodName = invocation.Method.Name,
                LogParameters = logparameters


            };
            return logDetailWithException;
        }
    }
}
