using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;   //getservice buradan geldi 
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache; //bunu benim çözmem lazım ctorda çalışmaz çünkü zincir şu şekilde ilerliyor web api business dataaccess aspect bambaşka bir zincirin içinde dolaysıı ile bağımlılık zincirinin içinde değil bu tarz durumlar için bir coremodule yazmıştık 
        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>(); 

            //dolayısı ile tüm dependency çözümlemelerini burada yapabilirim 
        }
        public void Add(string key, object value, int duration)
        {
            //biizm burada memorycachemiz var ve işlemlerimiz de onu kullanıcaz 
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));  //ne kadar süre verirsek ilgili data o kadar cachede kalacak 
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);    //get et ama hangi türde onu döndüreyim ve keyi veriyorum 
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key); // tip belirtmeden de getirilebilri ama bizim tip dönüşümü yapmamız gerekiyor ama boxing yapmalıyız 

        }

        public bool IsAdd(string key)
        {
           //bellekte böyle bir cache var mmı 
          return  _memoryCache.TryGetValue(key, out _);  
            //ben datayı istemiyorum bana yalnızca belekte olup olmadığını söyle sana zahmet olmasın 
            // bellekte olup olmadığı bilgisini bana versen yeterli herhangi bir şey döndürmene gerek yok 
            //burada bir değer veriyosun o sana set edip geri döndürüyor ben bir şey döndürsün istemiyorum 


        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key); 
            //aslında burası bir mülakatta olaya hakim biri varsa bu tip sorular ve ona benzer sorular sorarlar 
            //microsoftun elemanı zaten yazmış .neti core geliştirienler zaten yazmış  hepsinin karşılığı var ben niye gidip metot yapıp yapıp duruyorum sektörde baya bir kooda bu şekilde ama ben bir metodun içine koyuyorum bunu benim derdim yalnızca microsoftun cacheni eklemek değil yarın öbür gün başka başka cacheler kullanabilim farklı bir cache de patlamayayım diye .net coredan gelen kodu kendime uyarlıyorum 
             //Adapter pattern :var olan bir sistemi kendi sistemime uyarlıyorum diyorum ki sen benim sistemime göre çalışıcaksın 
        }

        public void RemoveByPattern(string pattern)
        {
            //remove by pattern bellekten silmeye yarar çalışma annında yani elimde bir sınıfın instance var ve ona çalışma anında müdahale etmek istiyorum bunu reflexionla yaparız elimizde olan nesnelere müdahale ederiz 
            //kodu çalışma anında oluşturma dolayısı ile ben diyorum ki ;
            //ilk olarak bellekte memerycahe çekiyorum git belleğe bak bellekte memerycache türünde olan EntiesColleciton bunu biz microsft diyor ki ben cacheleme yaptıpım da bunu EntriesCollection bir şeyin içine atıyorum dökümantasyodan biliyoruz biz bunu  definitonu memerycache olanları bul sonrasında her bir cache eleamanını gez ve her bir cache elemanından   var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList(); bu kurala uyanlar bu silme işlemi gerçekleştirirken vereceğim kuraldır 
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }   
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //pattern bu şekilde oluşturulur anahtarlardan benim gönderdiğim anahtara eşit olanları getir kuralda diyebilir örneğin içinde category olanları getir onu keysToRemove içine at foreach ile tek tek geziyorum key ile uyanları buluyorum onlarıda remove diyerek bellekten uçuruyorum 
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
