using ECommerceApp.Api.DTOs.Products;

namespace ECommerceApp.Api.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int productId);
        Task<ProductDto> CreateProductAsync(CreateProductDto productDto);
        Task<ProductDto?> UpdateProductAsync(int productId, UpdateProductDto dto);
        Task<bool> DeleteProductAsync(int productId);
    }
}