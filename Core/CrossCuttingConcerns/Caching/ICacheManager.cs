using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        //burası bizim tüm cache implemantasyonlarımızı yapacağımız yer  biz inmemory cache kullanıcaz yarın bir gün reddisi kullanmak istersen onu da implmenete edebilriisn teknoloji bağımsız interface
        void Add(string key, object value, int duration);
        //1:key-value şeklinde olacağını söyledik burası basedir ve dataların base de object olduğundan burasını object olarak geçeriz 
        //2:Cache de ne kadar duracak bunun içinde duration adında parametre ekliyorum dk saat biz ne istiyorsak yapılabilir 
        //cacheden data getirmek bu data tek bir objectde olabilir bir listede olablir  bu yüzden t tipnde diyorum ama nasıl geçeceğini hangi tiple çalıştık hangi tip kullanıcaz ve hangi tipe dönüşeccek bunu söylüyoruz 
        T Get<T>(string key);
        //Diyoruz ki biz sana bir key vereyim sen bellekten o keye karşılık olan sen bellekten o keyin karşılığı olan datayı bana ver bunun  farklı alternatifleri de yazılabilir 
        // bizim şu noktaya bakmamız gerekiyor herhangi bir data cacheden mi gelmeli yoksa veri tabanından buna nasıl karar veririz? eğer cachede varsa oradan yoksa dbden getirilmeli ama onu gidip cache ekleriz 
          object Get(string key); 
        bool IsAdd(string key); //dolayısı ile cache de var mı kontrolü yapabilmemizi sağlar 
        void Remove(string key); //diyorum ki ben sana bir key versem onu cacheden uçurur musun amabazı metotlar parametrik ya hangi key vericem onu bilmiyorum bir sürü olabilir böyle bir durumda da pattern yazsam mesela desem ki ismi get ile başlayanları uçur isminde category olanları uçur 
        void RemoveByPattern(string pattern); // ben ona regular expiression versem kısaca bir pattern versem desen versem diyorum bu tür durumlarda bu bizi baya bir korucak 

    }
}
