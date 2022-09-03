using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user); //claimleri çek  
        void Add(User user);  //kullanıcı ekle 
        User GetByMail(string email); //verilen emaile göre kullnıcı getir 
    }
}
