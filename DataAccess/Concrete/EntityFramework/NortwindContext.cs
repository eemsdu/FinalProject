using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //Context:Db tabloları ile proje classlarını bağlamak
    public class NortwindContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            //projenin hangi veri tabanı ile ilişkili olduğunu söylediğim yer 
        {
            
            optionsBuilder.UseSqlServer(@"Server=BURAKELDUT\SQLEXPRESS;Database=Northwind;Integrated Security=true");
        }
    
        //hangi nesnem hangi nesneye karşılık gelecek bunu db setler ile yapıyorum  
        public DbSet<Product> Products { get; set; }
        //benim Product nesnemi  veri tabanımda ki products ile bağla demektir.
        //context hangi veri tabanına bağlanacağımı buldu
        //benim hangi classım hangisine eşit olacağını bul 
        public DbSet<Category> Categories { get; set;}

        public DbSet<Customer> Customers { get; set;}

        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims{ get; set; }

    }
}
