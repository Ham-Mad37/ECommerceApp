using System.Security.Claims;
using ECommerceApp.Api.DTOs;
using ECommerceApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController:ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }
        [HttpPost("items")]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.AddToCartAsync(userId, dto);
            return Ok(cart);
        }
        [HttpPut("items")]
        public async Task<IActionResult> UpdateCartItem(int productId, UpdateItemCartDto dto)
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.UpdateCartItemAsync(userId, productId, dto);
            if (cart == null)
                return NotFound();
            return Ok(cart);
        }
        [HttpDelete("items/{productId}")]
        public async Task<IActionResult> RemoveFormCart(int productId)
        {
            var userid = GetCurrentUserId();
            var item = await _cartService.RemoveFromCartAsync(userid, productId);
            if (!item)
            {
                return NotFound();
            }
            return NoContent();

        }

        private int GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userId!);
        }
    }
}

