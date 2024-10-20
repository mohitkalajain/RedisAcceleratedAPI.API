using RedisAcceleratedAPI.API.Helper;
using RedisAcceleratedAPI.API.Models;

namespace RedisAcceleratedAPI.API.Repository.Interface
{
    public interface IProductService
    {
        Task<ResponseVM> GetAsync();
        Task<ResponseVM> GetByIdAsync(int id);
        Task<ResponseVM> CreateAsync(Product product);
        Task<ResponseVM> UpdateAsync(int id, Product product);
        Task<ResponseVM> DeleteAsync(int id);
    }
}
