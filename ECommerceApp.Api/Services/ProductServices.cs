using AutoMapper;
using ECommerceApp.Api.Data;
using ECommerceApp.Api.DTOs.Products;
using ECommerceApp.Api.Models;
using ECommerceApp.Api.Services.Interfaces;

namespace ECommerceApp.Api.Services
{
    public class ProductService(AppDbContext context, IMapper mapper) : IProductService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            _context.Products.Add(productEntity);
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<ProductDto>(productEntity));
        }

        public Task<bool> DeleteProductAsync(int productId)
        {
           var product = _context.Products.Find(productId);
            if (product == null)
            {
                return Task.FromResult(false);
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = _context.Products.ToList();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Task.FromResult(productDtos);
        }

        public Task<ProductDto?> GetProductByIdAsync(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return Task.FromResult<ProductDto?>(null);
            }

            return Task.FromResult(_mapper.Map<ProductDto?>(product));
        }

        public Task<ProductDto?> UpdateProductAsync(int productId, UpdateProductDto dto)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return Task.FromResult<ProductDto?>(null);
            }

            _mapper.Map(dto, product);
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<ProductDto?>(product));
        }
    }
}