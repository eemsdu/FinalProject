using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        //priority:öncelik demektir ,hangi attribude önce çalışsın 
        //önce valuditon sonra loglama  sıralama yapmak istersek kullanılır 

        public int Priority { get; set; }
        //hangi attribute önce çalışsın istiyorsak önce loglama sonra validation vs. öncelik 
        //bu base her yerde var  

        public virtual void Intercept(IInvocation invocation)
        {
            //İÇİ BOŞ BURAYI DOLDURUCAZ 
        }
    }
}

