using Microsoft.EntityFrameworkCore;
using ptuswph_backend.Models;
using System.Diagnostics;
using System.Text.Json;

namespace ptuswph_backend.Database
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
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
    }
}
