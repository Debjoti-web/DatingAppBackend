using DatingApp.API.Data;
using DatingApp.API.Interface;
using DatingApp.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.API.Extension
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,
        IConfiguration _Config)
        {
            services.AddDbContext<DataContext>(x=>x.UseSqlite(_Config.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddScoped<ITokenServices, TokenService>();
            return services;
        }
    }
}