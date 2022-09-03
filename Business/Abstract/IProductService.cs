using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService //iş kodunun soyutu 
    {
        IDataResult<List<Product>> GetAll();  
        // işlem sonucu ve mesajı da döndürmek istiyorum .
        IDataResult<List<Product>> GetAllCategoryId(int id);
        IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Update(Product product);
        IResult AddTransactionalTest(Product product);       
        //Restful --> Http --> Bir kaynağa ulaşmak için izlenilen yoldur 
        //Controller :Bizim sistemimizi kullanacak clientler bize hangi operasyonlarda bulunacaklarını ve/veya nasıl bulunacaklarını controllerda yazarız.
        //uygulamalarda tutarlılığı korumak için yaptığımız yöntem örneğim benşm hesambımda 100 tl var kereme 10 tl sini verdim bende 90 tl kalması kereminde parasının 10 tl artması gerekmektedir bu şekilde update olmalıdır yani aynı süreçte 2 tane veri tabanı işlemi var fakat benim hesabımdan giderken sıkıntı yok gitti lakin keremin hesabına giderken sistem hata verdi sistemin paramı geri iade etmesi gereklidir işlemleri geri alması gerekiyor yani bu nasıl yapılıyor bakalım 
     
    }
}
