using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        //test yaparken uyduruk token veya başka bir teknikle üretrmek istersek kullanırız 
        AccessToken CreateToken(User user,List<OperationClaim> operationClaims);
        //burası token üretilecek yer kim için token işte parametrede verdiğimiz gibi kullanıcı ve bu koullanıcı hangi yetki operationclaime sahip olsun yine parametrede veriririz 

    }
}
