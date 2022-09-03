using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //aspect
    {   
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //defensive coding :savunma odaklı kodlama 
            //rastgele class gönderilmesi engellendi 
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil ");
            }
            // karar alma 
            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            //reflexsion :çalışma anında bir şeyleri çalıştırabilmeyi sağlar  : Çalışma anında Instance oluşturma 
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //örneğin bir şeyi çalışma anında newlemek istersek
            //product validatorun git base typenı bul onunda generic argümanlarından ilkini bul 
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //ve o argümanlarından ilgili metodun parametrelerini bul 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}


