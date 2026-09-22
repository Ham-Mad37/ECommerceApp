using ECommerceApp.Api.Data;
using ECommerceApp.Api.DTOs;

namespace ECommerceApp.Api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CheekoutAsync(int userId);
        Task<IEnumerable<OrderDto>> GetMyOrdersAsync(int userId);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> UpdateOrderStatusAsync(int orderId, OrderStatus status);
    }
}