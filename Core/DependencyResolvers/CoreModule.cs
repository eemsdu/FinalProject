using Autofac.Core;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule   
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache(); 
            //.netin kendisinin Imemorycachin karşılığı var artık ama benim kendi memorycachemi kullanmak istersem de onu da yaptık sen yarın öbür gün redise geçersen Memorycachemaager yerine rediscache manager yazarız
            //Burası hazır bir enjectşon bu arka plan da hazır bir Icache manager instahance oluşturur
            //bellekte Ioc de kullanmamız için bir instancemız var 
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();
        }
        //bu bizim uygulama seviyesinde servis bağımllıklarımızı çözeceğimiz yer startup progrma cs gibi yer  yerine buraya gelicez
         //burada servicecolectionlarımıza şunu söylememiz gerekiyor birisi senden ICche manager isterse memorycache manager ver  
    }
}
