using ECommerceApp.Api.Data;

namespace ECommerceApp.Api.DTOs
{
    public class OrderDto
    {
        public int  Id { get; set; }
        public DateTime CreateAt { get; set; } 
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}