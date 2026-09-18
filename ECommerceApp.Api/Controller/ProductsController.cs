using AutoMapper;
using ECommerceApp.Api.DTOs.Products;
using ECommerceApp.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.Api.Models;
using ECommerceApp.Api.Services.Interfaces;
using ECommerceApp.Api.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceApp.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        //Get All Products
        [HttpGet]
        public async Task<ActionResult> GetProducts([FromQuery] ProductQuery query)
        {
            var (data,totalCount) = await productService.GetAllProductsAsync(query);
            var response = new
            {
                data,
                totalCount,
                query.Page,
                query.PageSize
            };
            return Ok(response);
        }
        // Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto dto)
        {
            var product = await productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
       [Authorize(Roles="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            var product = await productService.UpdateProductAsync(id, dto);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
       [Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productService.DeleteProductAsync((id));
            if (product == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }

       
    }
}
