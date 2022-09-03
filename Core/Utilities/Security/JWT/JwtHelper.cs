using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;  //oluşturmak serileştirmek ve jwt leri doğrulamak için kullanılan bir yapıdır 
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } //apideki appsettingteki degerleri okumaya yarar
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration; //tokennın geçersizleşeceği yer 
         
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //app settingsdeki bölümü hangi bölümü token options bölümünü ve onu tokenoptions sınıfı değerleri ile maple 
            //kısaca app settins.json daki audience ı sınıfımızda ki audienca , ıssuerı tokenoptionsda ki ıssuera tek tek atama yapar
            //json to class

        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            //bana bir user ve token bilgisini ver ben ona göre bir token oluşturayım
            //json to class yaptık dolayısı ile elimizde tüm bilgiler var buna göre eşitleme yapabiliriz 
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); //şimdi ki zamana dakika ekle ne kadar ? token options'daki dakikayı ekle  
            //Ben bunu oluştururken bir security key yani güvenlik anahtarına ihtiyacım var sen kafanı takma benim yazdığım securitykeyhelper var onun da CreateSecuritykey'i var tokenoptionsdaki security keyi kullanarak özel anahtarı oluştur 
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            //artık elimde bu tokenı oluşturucak güvenlik anahtarım da mevcut daha sonra bize hangi algoritmayı kullanayım diyor ve anahtara nedir ?
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            //sen bunun için de kafanı yorma benim kullanacağım algoritma ve securitykeyi de bu metot içine gömdüm onu da ordan al 

            var jwt = CreateJwtSecurityToken(_tokenOptions, user,signingCredentials,operationClaims);
            //her şey hazır artık jwt oluşturabilirim onun içinde createjwtsecuritytoken metodu oluşturuyorum 
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            //bu napıyo ? jwtsecuritytoken üretmeye,oluşturmaya yarıyor  issuer audience expires claim notbefore ve signingcredentials bilgileri kullanarak 
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims), //claim oluşumu için de bir metot yaptık oraya gidiniz 
                signingCredentials: signingCredentials
            );
            return jwt;
        }
         
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            //burasi icin extensions yazacagiz yani genisletecegiz
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
            return claims;
        }
    }
}