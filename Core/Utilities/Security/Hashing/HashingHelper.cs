using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; 
                //değşmeyen değer standart olucak tekrar bu passwordün çözülmesi için standart olması gerekiyor 
                passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash,  byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
                //bu algoritma bizden bir salt bekliyor o yüzden beizde bu keyi yani saltı parametre olarak vermeliyiz 
            {
                //aynen yukarıda yaptığımız gibi passwordü haslememiz gerekiyor ki karşılaştırabilelim bu şuan kullanıcının girdiği parola sonrasında girdiği yani sisteme tekrardan girmeye çalışıyor 
               var  computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //hesaplanan hash (kullanıcının vermiş olduğu hashe göre hesapladığımız hash) burada karşılaştırma yaparken parametrede vermiş olduğumuz salti kullan muhakkak bunu söylüyoruz
                //oluşan hesaplanan hash byte array türündedir dolayısı ile bunu karşılaştırabilmek için for döngüsü kullanmamız gerekiyor 
                for (int i = 0; i < computedHash.Length; i++)
                {
                    //diyoruz ki hesaplanan hashin bütün değerlerini tek tek dolaş 
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false; //iki değer birbiri ile eşleşmiyor 
                    }
                }
                return true;
            }
            //bu metot sonradan sisteme giren kişinin ilgili salta göre oluşan hashini karşılatırır veri tabanında ki hash ile uyumlu mu değil mi kontrol ettiğimiz yer 
           //bu iki değer birbiri ile eşleşirse true döner 
        }
    }
}
