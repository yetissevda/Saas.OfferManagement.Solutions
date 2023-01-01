using System;
using System.Linq;
using Castle.DynamicProxy;
using FluentValidation;
using Saas.Core.CrossCuttingConcerns.Validation;
using Saas.Core.Utilities.Interceptors;
using Saas.Core.Utilities.Messages;

namespace Saas.Core.Aspect.Autofac.Validation
{
    public class ValidationAspect :MethodInterception
    {
        private readonly Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception(AspectMessages.WrongValidatonType);
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            if (_validatorType.BaseType != null)
            {
                var entityType = _validatorType.BaseType.GetGenericArguments()[0];
                var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
                foreach (var entity in entities)
                {
                    ValidationTool.Validate(validator,entity);
                }
            }
        }
    }
}
