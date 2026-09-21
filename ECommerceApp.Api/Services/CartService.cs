using AutoMapper;
using ECommerceApp.Api.Data;
using ECommerceApp.Api.DTOs;
using ECommerceApp.Api.Models;
using ECommerceApp.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Api.Services
{
    public class CartService:ICartService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CartService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartDto> AddToCartAsync(int userId, AddToCartDto dto)
        {
            var productExist = await _context.Products.AnyAsync(p => p.Id == dto.ProductId);
            if (!productExist)
            {
                throw new KeyNotFoundException("Product not found!");
            }

            var cart = await GetOrCreateCartAsync(userId);
            var existingItem = cart.CartItems.FirstOrDefault(i => i.productId==dto.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem()
                {
                    productId = dto.ProductId,
                    Quantity = dto.Quantity
                });
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await  GetOrCreateCartAsync(userId);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var item = cart.CartItems.FirstOrDefault(i => i.productId == productId);
            if (item == null)
            {
                return false;
            }
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CartDto?> UpdateCartItemAsync(int userId, int productId, UpdateItemCartDto dto)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var item = cart.CartItems.FirstOrDefault(i => i.productId == productId);
            if (item == null)
            {
                return null;
            }
            item.Quantity = dto.Quantity;
            await _context.SaveChangesAsync();
            return _mapper.Map<CartDto>(cart);
        }

        private async Task<Cart> GetOrCreateCartAsync(int userId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart != null)
            {
                return cart;
            }

            cart = new Cart
            {
                UserId = userId
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return await _context.Carts.Include(c => c.CartItems).ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}