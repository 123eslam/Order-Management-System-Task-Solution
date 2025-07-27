using OrderManagementSystemTask.BLL.Dtos.ProductDto;

namespace OrderManagementSystemTask.BLL.Services.ProductServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();
        Task<ProductResultDto> GetProductByIdAsync(int id);
        Task<ProductResultDto> CreateProductAsync(ProductResultDto productDto);
        Task<ProductResultDto> UpdateProductAsync(int id, ProductResultDto productDto);
    }
}
