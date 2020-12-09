using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Extension
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,
        IConfiguration _Config)
        {
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(O => 
           {
               O.TokenValidationParameters=new TokenValidationParameters
               {
                   ValidateIssuerSigningKey=true,
                   IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["TokenKey"])),
                   ValidateIssuer=false,
                   ValidateAudience=false
               };
           });
           return services;
        }
    }
}