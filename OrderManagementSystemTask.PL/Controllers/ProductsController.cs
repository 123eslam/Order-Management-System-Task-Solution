using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystemTask.BLL.Dtos.ProductDto;
using OrderManagementSystemTask.BLL.Services.ProductServices;
using System.Net;

namespace OrderManagementSystemTask.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResultDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProducts()
        {
            var products = await productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductResultDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(ProductResultDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResultDto>> CreateProduct(ProductResultDto productDto)
        {
            var createdProduct = await productService.CreateProductAsync(productDto);
            return Ok(createdProduct);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductResultDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResultDto>> UpdateProduct(int id, ProductResultDto productDto)
        {
            var updatedProduct = await productService.UpdateProductAsync(id, productDto);
            return Ok(updatedProduct);
        }
    }
}
