using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

namespace ConsoleUI_
{
    public class Program
    {
         
        static void Main (string[] args )
        {
            //Data Transformation object
            //IoC
            ProductTest();
            CategoryTest();
            //CategoryTest();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);

            }
        }
        private static void ProductTest()
        {
            ////ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

            ////bana hangi veri yöntemi ile çalıştığını söylemen lazım 
            //var result = productManager.GetProductDetails();
            //if (result.Success==true)
            //{
            //    foreach (var product in productManager.GetProductDetails().Data)
            //    {
            //        Console.WriteLine(product.ProductName + "/" + product.CategoryName);
            //    }

            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}
           
        }
    }
}
