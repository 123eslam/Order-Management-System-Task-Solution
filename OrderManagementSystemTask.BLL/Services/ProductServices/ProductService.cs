using OrderManagementSystemTask.BLL.Dtos.ErrorDtos;
using OrderManagementSystemTask.BLL.Dtos.ProductDto;
using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.UnitOfWork;

namespace OrderManagementSystemTask.BLL.Services.ProductServices
{
    public class ProductService(IUnitOfWork _unitOfWork) : IProductService
    {
        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepostory.GetAllAsync();
            return products.Select(p => new ProductResultDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock
            });
        }
        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepostory.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }
            return new ProductResultDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }
        public async Task<ProductResultDto> CreateProductAsync(CreateOrUpdateProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Stock = productDto.Stock
            };
            _unitOfWork.ProductRepostory.Add(product);
            await _unitOfWork.CompleteAsync();
            return new ProductResultDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }
        public async Task<ProductResultDto> UpdateProductAsync(int id, CreateOrUpdateProductDto productDto)
        {
            var product = await _unitOfWork.ProductRepostory.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;
            _unitOfWork.ProductRepostory.Update(product);
            await _unitOfWork.CompleteAsync();
            return new ProductResultDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }
    }
}
