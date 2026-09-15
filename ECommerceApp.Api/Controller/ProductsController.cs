using AutoMapper;
using ECommerceApp.Api.DTOs.Products;
using ECommerceApp.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.Api.Models;

namespace ECommerceApp.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(AppDbContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await context.Products.ToListAsync();
            var result = mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var result = mapper.Map<ProductDto>(product);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto dto)
        {
            var product = mapper.Map<Product>(dto);
            context.Products.Add(product);
            await context.SaveChangesAsync();
            var result = mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
           mapper.Map(dto, product);
            await context.SaveChangesAsync();
            var result = mapper.Map<ProductDto>(product);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
