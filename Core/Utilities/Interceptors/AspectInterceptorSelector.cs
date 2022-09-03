using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttirubutes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttirubutes=type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();  
            classAttirubutes.AddRange(methodAttirubutes);
            return classAttirubutes.OrderBy(x => x.Priority).ToArray();
          ;
        }
    }
}
