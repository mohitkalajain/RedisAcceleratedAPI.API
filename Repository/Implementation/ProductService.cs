﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<ResponseVM> GetProductsAsync()
        {
            // Check if data is in Redis cache
            var cacheKey = "products";
            var cachedProducts = await _redisDb.StringGetAsync(cacheKey);

            if (!cachedProducts.IsNullOrEmpty)
            {
                // Deserialize and return cached data
                return ResponseVMFactory.Success(JsonSerializer.Deserialize<List<Product>>(cachedProducts));
            }

            // get data from database
            var products = await _context.Products.ToListAsync();

            // Store data in Redis for future requests
            await _redisDb.StringSetAsync(cacheKey, JsonSerializer.Serialize(products), TimeSpan.FromMinutes(10));

            return ResponseVMFactory.Success(JsonSerializer.Serialize(products));
        }
    }
}
