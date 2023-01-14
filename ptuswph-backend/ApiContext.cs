using Microsoft.EntityFrameworkCore;
using ptuswph_backend.Models;

namespace ptuswph_backend
{
    public class ApiContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public ApiContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_configuration.GetConnectionString("Database"));
        }

        public DbSet<User> Users { get; set; }
    }
}
