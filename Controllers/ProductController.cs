using Microsoft.AspNetCore.Mvc;
using RedisAcceleratedAPI.API.Helper;
using RedisAcceleratedAPI.API.Models;
using RedisAcceleratedAPI.API.Repository.Interface;

namespace RedisAcceleratedAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetAsync();
            return Ok(result);
        }


        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var result = await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            var result = await _productService.UpdateAsync(id, product);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
