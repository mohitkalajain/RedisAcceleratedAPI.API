using RedisAcceleratedAPI.API.Helper;

namespace RedisAcceleratedAPI.API.Repository.Interface
{
    public interface IProductService
    {
        Task<ResponseVM> GetProductsAsync();
    }
}
