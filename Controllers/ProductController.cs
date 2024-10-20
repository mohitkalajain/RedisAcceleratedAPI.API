using Microsoft.AspNetCore.Mvc;
using RedisAcceleratedAPI.API.Repository.Interface;

namespace RedisAcceleratedAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("Products")]
        public async Task<IActionResult> GetProducts()
        => Ok(await _productService.GetProductsAsync());
    }
}
