using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        //ekstradan bir metodum mevcut çünkü burada join atıcaz 
        //benim istediğim evet bir kullanıcının sisteme girebilmesi ama bunun yanında claimleri de olsun diyorum veri tabanınıda operation claimlerini çekmek ist,yorum
    }
}
