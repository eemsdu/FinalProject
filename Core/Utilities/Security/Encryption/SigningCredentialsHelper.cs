using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            //jwt servislerinin web apinin kullanabileceği jwtlerin oluşturulabilmesi için elimizde olanlar yani bir sisteme giriş için elimizde olanlar imzalama diyebiliriz  
            //asp nete diyoruz kşi sen hashing işlemi yapıcaksın anahtar olarak bu security keyi kullan şifreleme olarakda güvenlik algoritmalrından hmac sha 512 yi kulllan diyoruz  
            return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
            //biz şifreleme yaparken hashing helperda hangi algoritmayı kullancağımızı söyledik aynı sisteme asp netin de ihtiaycı var  
            //hangi anhatar hangi algoritma kullanılacak o verilir 
            //sen bir sistem jwt sistemini yöneteceksin senin güvenlik anahtarın budur şifreleme algoritmanda budur credentials budur 
            //credentials:sisteme giris bilgilerin
        }
    }
}
