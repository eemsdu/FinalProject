using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        //işin içerisnde şifreleme olan yapılarda biz her şeyi byte array olarak verememiz gerekiyor 
        //asp .netin bu tokenları anlayacağı hale getirmemiz gerekiyor 
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
                //bizim bir anahtara ihtiyacımız var bunlar simetrik ve asimetrik olarak ayrılıyorlar dersimizin konusu değil araştır         
                //bizim appsetting de verdiğimiz security key i asp.netin anlayacağı formata çevirdik
                //appsettingsde bulunan uyduruk stringi direk parametre olarak geçemiyoruz byte array haline dönüştürğyor bu sınıf bytenı alıp onu simetrik bir güvevnlik anahtarına dönüştürüyor 
                //bu bizim jwt nin ihtiyaç duyduğu yapılar 
        }

    }
}
