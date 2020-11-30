using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    public class AccountController:BaseApiController
    {
       // private readonly IValidator<RegisterDto> _validator;
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context=context;
          //  _validator=validator;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterDto register)
        {
           //var validator=new RegisterDtoValidator();
           // var result= validator.Validate(register);
           // if(!result.IsValid)
           // {
           //     return BadRequest(new { ErrorCode=400, message=""});
           // }
            if(await UserExists(register.UserName))
            {
                    return BadRequest("UserName is taken");
            }
            //dispose the object as soon as the work is completed.
                using var hmac= new HMACSHA512();
                User newUser=new User
                {
                    UserName=register.UserName.ToLower(),
                    PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                    PasswordSalt=hmac.Key
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return newUser;
        }
        private async Task<bool> UserExists(string userName)
        {
                return await _context.Users.AnyAsync(ECKeyXmlFormat=>ECKeyXmlFormat.UserName==userName.ToLower());
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginDto login)
        {
            //SingleOrDefaultAsync throws exception if there is more than one response.
                var user= await _context.Users.SingleOrDefaultAsync(use=>use.UserName==login.UserName);
                if(user==null)
                {
                    return Unauthorized("Invalid UserName");
                }
                using var h=new HMACSHA512(user.PasswordSalt);
                var c= h.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
                for(int i=0;i<c.Length;i++){
                    if(user.PasswordHash[i]!=c[i])
                    return Unauthorized("Wrong Password");
                }
                return user;
        }
        
    }
}