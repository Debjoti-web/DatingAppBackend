using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DatingApp.API.Interface;
using DatingApp.API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Services
{
    public class TokenService : ITokenServices
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration _config)
        {
            _key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));

        }
        public string CreateToken(User user)
        {
            var claims= new List<Claim>
            {
                    new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };
            var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriber=new SecurityTokenDescriptor
            {
                   Subject=new ClaimsIdentity(claims),
                   Expires=DateTime.Now.AddDays(7),
                   SigningCredentials=creds
            };
            var tokenhandler=new JwtSecurityTokenHandler();
            var token=tokenhandler.CreateToken(tokenDescriber);
            return tokenhandler.WriteToken(token);
        }
    }
}