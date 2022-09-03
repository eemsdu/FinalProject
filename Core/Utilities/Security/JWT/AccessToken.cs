using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        //erişim anahtarı :anlamsız karakterlerden oluşan anahtar değeridir 
        //kullanıcı api üzerinden postmanden kullanıcı adı ve şifresini verecek biz de ona bir tane token ve ne zaman sonlanacağı bilgisini verecez 
        //bu access token bir class bizim bu accesstoken oluşturacak yapıya ihtiyacımız var 
        public string Token { get; set; } //jeton 
        public DateTime Expiration { get; set; } //jetonun bitiş zamanı 

    }
}
