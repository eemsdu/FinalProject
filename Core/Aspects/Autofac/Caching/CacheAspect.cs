
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            //string.Join linq operasyonu araya virgül koy ne için parametrelerin her biri için örneğin 2 parametreli bir metodu çalıştırırsak örnek vermek gerekirse ankara, 5 gibi string birleştirme yapar 
            //2 soru işareti şunu sağlar varsa soldakini ekle yoksa null olanı ekle gibi parametre null değilse parametreli ekle null ise methoda bu parametreleri vererek ekle ona göre key oluştur tek soru işareti string yapılabiliyorsa demektir arguments.select parametreleri listeye çevirirstring join ise virgül ile onalrı yanyana getirir 
                if (_cacheManager.IsAdd(key)) //oluşan key bellekte var mı kontrolü 
            {
                invocation.ReturnValue = _cacheManager.Get(key); //return değeri cache olsun 
                return;
            }
            //bu daha önce belekte var mı varsa cachden getirir yoksa veri tabanından getirir ama gidip cachede ekler 
            //yoksa metot proceed et devam ettir veri tabanına git oradan datayı al metot.dönendeğer key ve süre vermiş olduğumuz key süresi ile birlikte cache ekleme yaparız duration cache de bu data  ne kadar kalıcak 
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
    //bu aspect metodun hangi yolda olduğunu bulup metot ismi ile birlikte methodName adlı değişkene atılıyor daha sonra bu metodun parametresi var ise o seçilip bu değişkene eklenir ve bir key oluşur bu key bellekye var mı diye bakılır var ise direk döndürülür  yoksa veri tabanına gidip data alınıp bu key ile birlikte cache eklenir
    ////Nortwind.Business.IProductService.GetAll(2); örnek olarak bu şkeilde bir key oluşuyor 
}
