using Microsoft.EntityFrameworkCore;
using RedisAcceleratedAPI.API.Models;

namespace RedisAcceleratedAPI.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // You can add configurations here if needed
            base.OnModelCreating(modelBuilder);
        }
    }
}
