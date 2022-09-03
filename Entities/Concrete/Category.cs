using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category : IEntity
        // concrete klasöründeki sınıflar bir veri tabanı tablosuna eş değerdir
         
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
