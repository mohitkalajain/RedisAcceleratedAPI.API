using Microsoft.EntityFrameworkCore;
using RedisAcceleratedAPI.API.Data;
using RedisAcceleratedAPI.API.Helper;
using RedisAcceleratedAPI.API.Models;
using RedisAcceleratedAPI.API.Repository.Interface;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisAcceleratedAPI.API.Repository.Implementation
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IDatabase _redisDb;
        public ProductService(AppDbContext context, IConnectionMultiplexer redis)
        {
            _context = context;
            _redisDb = redis.GetDatabase();
        }

        public async Task<ResponseVM> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            await RefreshRedisCacheAsync();
            return ResponseVMFactory.Success(product);
        }

        public async Task<ResponseVM> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return ResponseVMFactory.NotFound(AppConstants.MessageKeys.PRODUCT_NOT_FOUND, 
                                                    StatusCodes.Status400BadRequest);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            await RefreshRedisCacheAsync();
            return ResponseVMFactory.Success(AppConstants.MessageKeys.PRODUCT_DELETED);
        }

        public async Task<ResponseVM> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                return ResponseVMFactory.NotFound(AppConstants.MessageKeys.PRODUCT_NOT_FOUND, StatusCodes.Status404NotFound);
            }
            return ResponseVMFactory.Success(product);
        }

        public async Task<ResponseVM> GetAsync()
        {
            // Check if data is in Redis cache

            var cachedProducts = await _redisDb.StringGetAsync(AppConstants.CacheKeys.Products);

            if (!cachedProducts.IsNullOrEmpty)
            {
                // Deserialize and return cached data
                return ResponseVMFactory.Success(JsonSerializer.Deserialize<List<Product>>(cachedProducts));
            }

            // get data from database
            var products = await _context.Products.ToListAsync();

            // Store data in Redis for future requests
            await UpdateRedisCache(products);

            return ResponseVMFactory.Success(JsonSerializer.Serialize(products));
        }

        public async Task<ResponseVM> UpdateAsync(int id, Product product)
        {
            if (id != product.Id)
            {
                return ResponseVMFactory.BadRequest(AppConstants.MessageKeys.ID_MISMATCH, StatusCodes.Status400BadRequest);
            }

            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExistsAsync(id))
                {
                    return ResponseVMFactory.NotFound(AppConstants.MessageKeys.PRODUCT_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                else
                {
                    throw;
                }
            }

            await RefreshRedisCacheAsync();
            return ResponseVMFactory.Success(product);
        }
        private async Task<bool> ProductExistsAsync(int id)
        {
            try
            {
                return await _context.Products.AnyAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }
        private async Task RefreshRedisCacheAsync()
        {
            var products = await _context.Products.ToListAsync();
            await UpdateRedisCache(products);
        }
        private async Task UpdateRedisCache(List<Product> products)
        {
            await _redisDb.StringSetAsync(
                AppConstants.CacheKeys.Products,
                JsonSerializer.Serialize(products),
                TimeSpan.FromMinutes(AppConstants.CacheDuration.ProductsCacheMinutes)
            );
        }

    }
}
