using ECommerceApp.Api.DTOs;

namespace ECommerceApp.Api.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(int userId);
        Task<CartDto> AddToCartAsync(int userId, AddToCartDto dto);
        Task<CartDto?> UpdateCartItemAsync(int userId, int productId, UpdateItemCartDto dto);
        Task<bool> RemoveFromCartAsync(int userId, int productId);
    }
}

