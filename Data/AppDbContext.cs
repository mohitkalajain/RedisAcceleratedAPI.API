using Microsoft.EntityFrameworkCore;
using RedisAcceleratedAPI.API.Models;

namespace RedisAcceleratedAPI.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
