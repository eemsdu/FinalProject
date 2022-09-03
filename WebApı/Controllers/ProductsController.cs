using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //ATTRIBUTE = Bir classla ilgili bilgi verme 
    public class ProductsController : ControllerBase
    {
        //loosely coupled :Gevşek bağımlılık ,soyut bağımlılık 
        //solid uyularak yapıldı...
        //naming convetion :İsimlendirme kuralı böyle olmasa da çalışırdı 
        //IOC CONTAİNER --Inversion of control = değişimin kontrolü anlamına gelir 
         IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
            //_productService = new ProductManager(); bu şekilde yazsaydık bağımlı olurduk 
            // IProductService productService=Bana bir tane manager ver demektir .Çünkü Iproductservice managerin referansını tutabilir 
        }
        //her metoda cache yazılmaz eticaret sistemlerinde kullanılabilir genellikle 
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //dependency chain--
            //swagger :dökümantasyon 
            var result= _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Product product)

        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }
    }
}
