using AutoMapper;
using ECommerceApp.Api.Data;
using ECommerceApp.Api.DTOs;
using ECommerceApp.Api.Models;
using ECommerceApp.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Api.Services
{
    public class OrderService:IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> CheekoutAsync(int userId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems)
                .ThenInclude(i => i.Product).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null || !cart.CartItems.Any())
            {
                throw new InvalidOperationException("Cart Is Empty.");
            }

            var order = new Order
            {
                UserId = userId,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                Items = cart.CartItems.Select(item => new OrderItem
                {
                    ProductId = item.productId,
                    ProductName = item.Product.Name,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Quantity
                }).ToList()
            };
            order.TotalAmount = order.Items.Sum(i => i.UnitPrice * i.Quantity);
            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDto>(order);
        }


        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.Include(c => c.Items)
                .OrderByDescending(c => c.CreatedAt).ToListAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetMyOrdersAsync(int userId)
        {
            var orders = await _context.Orders.Include(c => c.Items)
                .Where(c => c.UserId == userId).OrderByDescending(c=>c.CreatedAt).ToListAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);

        }

        public async Task<OrderDto?> UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.Include(c => c.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return null;
            }

            order.Status = status;
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDto>(order);
        }
    }
}

