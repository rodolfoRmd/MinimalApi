using Microsoft.EntityFrameworkCore;
using MinmalApi.Models;


namespace MinimalApi.Data
{
    
    public class AppDBContext : DbContext
    {
         private readonly IConfiguration _configuration;
        public DbSet<Todo> Todos { get; set; }

        public AppDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(_configuration.GetConnectionString("DefaultCon"));
    }
}