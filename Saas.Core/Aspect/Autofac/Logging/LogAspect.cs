using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Saas.Core.CrossCuttingConcerns.Logging;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net;
using Saas.Core.Utilities.Interceptors;
using Saas.Core.Utilities.IoC;
using Saas.Core.Utilities.Messages;

namespace Saas.Core.Aspect.Autofac.Logging
{
    public class LogAspect :MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLogType);
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation));
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase?.Info(GetLogDetail(invocation));
        }


        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters,
                User = _httpContextAccessor.HttpContext == null | _httpContextAccessor?.HttpContext?.User?.Identity?.Name == null ? "?" : _httpContextAccessor.HttpContext.User.Identity.Name

            };
            return logDetail;
            //return JsonConvert.SerializeObject(logDetail);

        }
    }
}
