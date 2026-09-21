namespace ECommerceApp.Api.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int  CartId { get; set; }
        public int  productId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        

    }
}