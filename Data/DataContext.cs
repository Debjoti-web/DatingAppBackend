using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    //Microsft removed assemblies from shared framework so need to install in entity framework core manually was 
    //available in 2.2
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<Value> Values{get;set;}
        public DbSet<User> Users{get;set;}
    }
}