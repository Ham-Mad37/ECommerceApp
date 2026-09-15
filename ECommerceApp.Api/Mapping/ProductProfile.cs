using AutoMapper;
using ECommerceApp.Api.DTOs.Products;
using ECommerceApp.Api.Models;
namespace ECommerceApp.Api.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}