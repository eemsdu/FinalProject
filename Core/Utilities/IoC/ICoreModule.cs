using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public interface ICoreModule //framework katmanınımız tüm projelerimizde kullanabiliriz 
    {
        void Load(IServiceCollection serviceCollection);    
        //genel bağımlılıkları yüklücek  ; bu arkdaşımız servisleri yüklücek diyoruz; biz ona service collectionuuzu vericez o yükleme işini yapacak 
    }
}
