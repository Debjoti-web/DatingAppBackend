using DatingApp.API.Models;

namespace DatingApp.API.Interface
{
    public interface ITokenServices
    {
         string CreateToken(User user);
    }
}