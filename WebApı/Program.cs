using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule() 
}); 
   //biz yarın öbğr core module gibi farklı modüller oluşuturursak buraya onu da ekleyebiliriz securitymodule vs...
//istediğim kadar modülü eklemek istiyorum 
//bu arkadaşa extensions yapacaz polimorfizm 


var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });
//Bana arka planda referans oluþtur 
//Kýsaca ýocler bizim yerimize newler 
/// controllera diyor ki sen baðýmlýlýk görürsen onun karþýlýðý virgülden sonra ki kýsýmdýr. bu bizim yerimize newler.Ýçerisinde data tutmuyorsak singleton kullanýrýz içerisinde veri tutmuyorsak 

//autofac,ninject,castlewindsor,structureMap,dryýnject,lightýnject -->ýoc container
//AOP --> Bir metotun önünde bir metotun sonunda bir metot hata verdiðinde çalýþan kod parçaçýðýdýr .
//postsharp 

//builder.Services.AddSingleton<IProductService,ProductManager>();
//builder.Services.AddSingleton<IProductDal,EfProductDal>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//buraya autofac kullanmak için  yazdýn önemli 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())

{
    app.UseSwagger();   
    app.UseSwaggerUI();

}
app.UseAuthentication(); 
//midleware asp .net yaşam döngüsünde hangi yapıların sırası ile devreye gireceğini söylüyoruz  token alamaz isek hata burada olabilir 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();