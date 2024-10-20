using Microsoft.EntityFrameworkCore;
using RedisAcceleratedAPI.API.Models;
namespace RedisAcceleratedAPI.API.Data.Seed
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(AppDbContext context, ILogger<DatabaseSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            if (!await _context.Products.AnyAsync())
            {
                _logger.LogInformation("Seeding database...");
                var products = Enumerable.Range(1, 10000).Select(i => new Product
                {
                    Name = $"Product {i}",
                    Price = (decimal)(i * 0.99)
                }).ToList();

                await _context.Products.AddRangeAsync(products);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Database seeded successfully.");
            }
            else
            {
                _logger.LogInformation("Database already contains products. Skipping seed.");
            }
        }
    }
}
