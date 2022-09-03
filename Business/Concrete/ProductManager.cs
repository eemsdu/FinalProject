using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
     
        ILogger _logger;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ILogger logger,ICategoryService categoryService)
        {
            _productDal = productDal;
          
            _logger = logger;
            _categoryService= categoryService;
        }

        [SecuredOperation("product.add,admin")] 
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]  //yeni ürün ekledim cache bozuldu bu yüzden db den getirir
        public IResult Add(Product product)
        {
           IResult result= BusinessRules.Run(CheckIfProductNameExist(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfCategoryLimitExceded());
            if (result!=null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }
        //manipilasyon yapan metotları cache ile yönetmek 
        //db ye elle data eklemek problem çoğu sistem bunu yapamaz businessı ara yüze yazar çünkü 

        //Business yalnız IProduct ,ICategory vb. soyut sınıfları  bilir burada somut sınıf tanımı yapılmaz.
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
           // soyut nesne ile bağlantı kurucaz.
           // varsa iş kodlarını yazıyoruz
           // bir iş sınıfı başka bir sınıfı newlemez onun yerine enjection yapılır
           //yetki ver çünkü kurallardan geçtim
            if (DateTime.Now.Hour == 24)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<Product>> GetAllCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
           
        }
        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == productId));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
       
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
         [ValidationAspect(typeof(ProductValidator))]
         [CacheRemoveAspect("IProductService.Get")] //bellekte ki içinde get olan tüm keyleri iptal et 
        public IResult Update(Product product)
        {
           
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
          
        }
         private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var categorygetproduct = _productDal.GetAll(x => x.CategoryId == categoryId).Count();
            if (categorygetproduct >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();//kullanıcıya data vermek gerekmez  bu kuraldan geçiyoruz çünkü 
        }
        private IResult CheckIfProductNameExist(string productName)
        {
            var result=_productDal.GetAll(x=>x.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
                //KATEGORİ LİMİTİ AŞILDI 
                //BU BİZİM PRODUCTIN KATEGORisi, SERVİSİ NASIL YPRUMLANIR ONUN OLAYI BU YÜZDEN BURADA YAZILIYOR KATEGORİ SERVİSTEN YAPILMAISNIN SEBEBİ BUDUR 
                 
            } 
            return new SuccessResult();
        }
        [TransactionScopeAspect]
        [PerformanceAspect(5)] //diyorum ki bu metodun çalışması 5 sn yi geçerse beni uyar 
        public IResult AddTransactionalTest(Product product)
        {
            //disposible pattern 
            Add(product); //ürünü ekledi 
            if (product.UnitPrice<10) //ürün eklendikten sonra başka bir işlem yaptı 
            {
                throw new Exception("");
            }  
            Add(product);
            return null;     
        }
    }
}
