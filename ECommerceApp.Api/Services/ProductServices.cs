using AutoMapper;
using ECommerceApp.Api.Data;
using ECommerceApp.Api.DTOs;
using ECommerceApp.Api.DTOs.Products;
using ECommerceApp.Api.Models;
using ECommerceApp.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<(IEnumerable<ProductDto>Data,int TotalCount)> GetAllProductsAsync(ProductQuery querey)
        {
            var productQuery = _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(querey.Search))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(querey.Search));
            }
            if (querey.MinPrice.HasValue)
            {
                productQuery = productQuery.Where(p => p.Price >= querey.MinPrice.Value);
            }
            if (querey.MaxPrice.HasValue)
            {
                productQuery = productQuery.Where(p => p.Price <= querey.MaxPrice.Value);
            }
            var totalCount = await productQuery.CountAsync();
            productQuery = querey.SortBy?.ToLower() switch
            {
                "price" => querey.Descending
                    ? productQuery.OrderByDescending(p => p.Price)
                    : productQuery.OrderBy(p => p.Price),
                "name" => querey.Descending
                    ? productQuery.OrderByDescending(p => p.Name)
                    : productQuery.OrderBy(p => p.Name),
                _ => productQuery.OrderBy(p => p.Id)
            };
            var product = await productQuery
                .Skip((querey.Page - 1) * querey.PageSize)
                .Take(querey.PageSize)
                .ToListAsync();
            var data = _mapper.Map<IEnumerable<ProductDto>>(product);
            return (data, totalCount);


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